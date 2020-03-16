using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LibForCore.ConfigJson;
using Microsoft.Extensions.DependencyInjection.Extensions;
using LibForCore.LogHelp;
using LibForCore.SkypeHelp;
using Main.Services;
using Main.MidWare;
using DataModel.SettingModel;
using BLL.Interface;
using BLL.Service;
using DAL.Interface;
using DAL.Repository;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http;
using System.Net;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;
using DataModel.Context;
using GraphQL;
using BLL.GraphQL.GraphQLSchema;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using BLL.GraphQL.GraphQLQueries;
using GraphQL.Types;
using BLL.GraphQL.GraphQLTypes;

namespace Main
{
    public class Startup
    {

        private readonly ILog log = new NLogger("Startup");
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)         // 讀取組態設定檔時，會先讀取 settings.json 並設定 optional=false，指定該檔為必要檔案；再讀取 settings.{env.EnvironmentName}.json 檔案
                .AddDecryptJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)  // 自定義的延伸method ,解密appsettings中有加密的部分
                .AddEnvironmentVariables();   //載入環境變數

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(opt =>
                opt.UseSqlite("Data Source=database.sqlite"));
                //opt.UseSqlServer(Configuration.GetConnectionString("sqlConString")));
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            // 註冊IDependencyResolver物件
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            // 註冊AppSchema物件
            //services.AddScoped<AppSchema>();
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<ISchema, AppSchema>();
            services.AddScoped<AppQuery>();
            services.AddTransient<OwnerType>();
            //services.AddScoped<RootMutation>();
            // 註冊GraphQL以及GraphTypes物件，其中AddGraphTypes()會自動幫我們註冊所有GraphQL Types，省下我們個別註冊每一個GraphQL Type的時間。
            // services.AddGraphQL(o => { o.ExposeExceptions = false; })
            //     .AddGraphTypes(ServiceLifetime.Scoped);

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers()     // for 3.1
                .AddNewtonsoftJson();     // for 3.1   

            // Swagger
            services.AddSwaggerGen(c =>
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "Api.xml");
                c.IncludeXmlComments(filePath);
                c.SwaggerDoc(
                    // name: 攸關 SwaggerDocument 的 URL 位置。
                    name: "v1",
                    // info: 是用於 SwaggerDocument 版本資訊的顯示(內容非必填)。
                    // [3.1]
                    info: new OpenApiInfo
                    {
                        Title = "DotnetCore3 API",
                        Version = "1.0.0",
                        Description = "",
                        
                    }

                );
            });

            // 提供記憶體快取
            services.AddMemoryCache();
            // 自定義CustomLogger(打包NLog) 替換微軟的實作底層
            services.Replace(new ServiceDescriptor(typeof(ILogger<>), typeof(CustomLogger<>), ServiceLifetime.Singleton));
            // 以強型別物件解析appsettings , 由於每個專案有不同設定值, 因此以繼承AppSetting的class來承接
            services.Configure<AppSettingMain>(Configuration);
            // 註冊自行實作的定時任務
            //services.AddHostedService<TimerTaskService>();    
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, TimerTaskService>();    // 注意Namespace需額外指定才不會造成IHostEnvironment混淆
            // db
            services.AddTransient<IBaseRepository, BaseRepository>();
            services.AddTransient<IBaseService, BaseService>();

            // Polly , Retry策略設定 , 若不需動態調整設定值可在此定義
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, interval => TimeSpan.FromSeconds(5));
            var timeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(5));

            //AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
            // 使用httpclient factory

            services.AddHttpClient("Common", client =>
            {
                //client.BaseAddress = new Uri("");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                //client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Add("User-Agent", "Poller");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

            })
            .AddPolicyHandler((service, request) => HttpPolicyExtensions.HandleTransientHttpError()

                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(5)
                },
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    service.GetService<ILogger<Startup>>()
                        .LogWarning($"[ Polly Retry ] - {context["RequestUrl"]}，Delaying for {timespan.TotalMilliseconds}ms, then making retry {retryAttempt}.");
                }

            ))
            //.AddPolicyHandler(retryPolicy)           
            .ConfigurePrimaryHttpMessageHandler(c =>
            {
                var handler = new HttpClientHandler();

                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
                //
                //if (handler.SupportsAutomaticDecompression)
                //{
                handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                //}

                return handler;
            });

            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env, IHostApplicationLifetime appLifetime)
        {
            // 通用例外攔截
            // Middleware 的註冊順序很重要，越先註冊的會包在越外層。把 ExceptionMiddleware 註冊在越外層，能涵蓋的範圍就越多。
            // 須注意在環境是Development時,例外會先被UseDeveloperExceptionPage攔到
            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // 生命週期事件
            appLifetime.ApplicationStarted.Register(() =>
            {
                log.Info("AppLifetime - Start");
            });
            appLifetime.ApplicationStopping.Register(() =>
            {

                log.Info("AppLifetime - Stopping");
            });
            appLifetime.ApplicationStopped.Register(() =>
            {

                log.Info("AppLifetime - Stopped");
            });


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    // url: 需配合 SwaggerDoc 的 name。 "/swagger/{SwaggerDoc name}/swagger.json"
                    url: "/swagger/v1/swagger.json",
                    // description: 用於 Swagger UI 右上角選擇不同版本的 SwaggerDocument 顯示名稱使用。
                    name: "API Utility"
                );
            });
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            // 在Configure中加入GraphQL Middleware，以及GraphQL Client端測試用Middleware GraphQLPlayground
            //app.UseGraphQL<AppSchema>();
            //app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());

            //app.UseMvc();

            // [3.1]
            app.UseRouting();
            // [3.1]
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                                            name: "default",
                                            pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
            });
        }
    }
}
