using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LibForCore.LogHelp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LibForCore.StopwatchHelp;
using System.Text;
using System.Net;
using Microsoft.Extensions.Options;
using DataModel.SettingModel;
using System.Collections.Concurrent;
using System.IO;
//using Microsoft.Extensions.Configuration.Binder;
using BLL.Interface;
using BLL.Service;
using System.Net.Http;
//using LibForCore.SkypeHelp;
using DataModel;
using System.Globalization;
using LibForCore.ExceptionHandle;
using Polly.Extensions.Http;
using Polly;

namespace Main.Services
{
    /// <summary>
    /// 實作定時任務
    /// </summary>
    public class TimerTaskService : IHostedService, IDisposable
    {
        #region < Private Members

        private string DateType { get; set; }

        private int ServiceInterval { get; set; }

        private int HttpClientTimeout { get; set; }

        /// <summary>
        /// Poller need Initial request to Manager API
        /// </summary>
        /// <value></value>
        private bool InitialFalg {get;set;}

        private readonly CustomLogger<TimerTaskService> _log;
        private readonly AppSettingMain _config;
        private IBaseService _BaseService;
        //private Timer _timer;
        private System.Timers.Timer _timer = new System.Timers.Timer();
        private object locker = new object();

        public IHttpClientFactory _httpClientFactory { get; private set; }
        //private ISkypeSender _skypeSender;
        public IHostEnvironment _hostingEnvironment { get; private set; }

        private int _IsRunning = 0;             // timer 使用的Interlock



        /// <summary>
        /// timer執行的時間間隔
        /// </summary>
        private int TimerInterval { get; set; }  // 單位 ms
        /// <summary>
        /// Timer計時計數器
        /// </summary>
        private int TimerCounter { get; set; }



        #endregion


        #region < 初始化

        public TimerTaskService(ILogger<TimerTaskService> log, IOptions<AppSettingMain> config, IBaseService BaseService, IHttpClientFactory httpClientFactory,
                                 IHostEnvironment hostingEnvironment)
        {
            _log = log as CustomLogger<TimerTaskService>;
            _config = config.Value;
            TimerInterval = 1000;


            // 快取資料
            _BaseService = BaseService;
            _hostingEnvironment = hostingEnvironment;       // 為了取得wwwroot path，要在 GetConfigValues()之前
            GetConfigValues();

            _httpClientFactory = httpClientFactory;
            //_skypeSender = skypeSender;

            //ThreadPool.SetMinThreads(10,10);  // test

            try
            {
                
            }
            catch (Exception ex)
            {

                _log.Error("TimerTaskService ctor 例外", ex);
            }


        }

        #endregion

        #region <IHostedService實作

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _log.Info("Timed Background Service is starting.");
            //_TimerCancelToken = cancellationToken;
            //_timer = new Timer(DoWork, null, TimeSpan.Zero,
            //    TimeSpan.FromMinutes(ServiceInterval));                  // Timer 間隔時間設定

            _timer.Interval = TimerInterval;
            _timer.Elapsed += DoWork;
            _timer.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _log.Info("Timed Background Service is stopping.");

            //_timer?.Change(Timeout.Infinite, 0);

            _timer.Stop();

            return Task.CompletedTask;
        }

        #endregion

        #region < 依功能實作

        /// <summary>
        /// Timer Callback
        /// </summary>
        /// <param name="state"></param>
        //private async void DoWork(object state)
        private async void DoWork(object state, System.Timers.ElapsedEventArgs e)
        {

            try
            {
                if (Interlocked.CompareExchange(ref _IsRunning, 1, 0) == 0)    // 確保前一次定時event的處理還沒完成前,不會產生數據錯亂
                {
                   

                    // 計時檢查
                    TimerCounter += 1;
                    
                    // 
                    if (TimerCounter * TimerInterval / 1000m < ServiceInterval * 10) // ServiceInterval以分為單位
                    {       
                        // 時間未到
                         _IsRunning = 0;                        
                        return;
                    }
                    else
                    {
                        //
                    }
                   
                    
                   
                    //await Task.Delay(15000);
                    TimerCounter = 0;
                    _log.Info("離開Timer迴圈");
                    _IsRunning = 0;

                }
            }
            catch (Exception ex)
            {
                _log.Error("TimerTaskService - DoWork - Exception: ", ex);
                _IsRunning = 0;
                TimerCounter = 0;
                //await _skypeSender.SendToGroup($"Base - 新版抓流量程式", $"TimerTaskService發生例外:{ex.Message}");

            }
            
        }

        

       

       

        private async Task<string> HttpClientWithPolly(HttpMethod method ,string finalPath )
        {
            string finalJson ="";
            var httpClient = _httpClientFactory.CreateClient("Common");
            httpClient.Timeout = TimeSpan.FromSeconds(HttpClientTimeout);  
            HttpResponseMessage responses = null;
            
             // 為了讓Polly可以log請求的Url做以下設定
            var context = new Polly.Context();
            context["RequestUrl"] = finalPath;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, finalPath);
            request.SetPolicyExecutionContext(context);

            
            try
            {
	            responses = await httpClient.SendAsync(request);

                if (!responses.IsSuccessStatusCode)
                {
                    //GetEdgeTrafficFail(cdnInfo, (int)responses.StatusCode, finalPath, "網路問題");
                    _log.Warn($"請求的網址: {finalPath}，no 200 code");
                    throw new HttpRequestException($"IsSuccessStatusCode:false,{responses.StatusCode.ToString()}");
                }

                // ConfigureAwait(false)是設定為不需要context同步 ,是為了不浪費記憶體空間
                //using (var stream = await responses.Content.ReadAsStreamAsync().ConfigureAwait(false))
                using (var stream = await responses.Content.ReadAsStreamAsync())
                using (var sr = new StreamReader(stream))
                {

                    finalJson = sr.ReadToEnd();
                    _log.Debug($"Url:{finalPath} - 收到response");
                   
                    return finalJson;
                }
            }
            catch (HttpRequestException he)
            {
                return "fail";
            }
            catch (Exception ex)
            {
                return "ex fail";
            }
        }

        /// <summary>
        /// 取得存在DB裡的相關設定值 , 當DB裡的值有變動時, 也可以透過呼叫此method來更新快取
        /// </summary>
        private void GetConfigValues()
        {
            InitialFalg = true;

            DateType = "yyyyMMddHHmm";
            ServiceInterval = 1;
            HttpClientTimeout = _config.HttpClientTimeout;
            //SkypeGroup = _config.SkypeGroup;

            _log.Info("重新取得DB設定資料)");
        }




        #region API接口使用的Method





        /// <summary>
        /// 提供外部事件呼叫 , 主要是後台web會發出這樣的請求
        /// </summary>
        public void UpdateDataHandler()
        {
            //StopAsync(new CancellationToken());

            GetConfigValues();

            //StartAsync(new CancellationToken());
            _log.Info("Timer接收到重取設定值命令");
        }

        #endregion


        #endregion


        public void Dispose()
        {
            _timer?.Dispose();
            _log.Info("Timer執行dispose");
        }
    }
}



