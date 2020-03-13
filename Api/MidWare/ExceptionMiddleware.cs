using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Main.MidWare
{
    /// <summary>
    /// 全站通用例外攔截
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                bool isApi = Regex.IsMatch(context.Request.Path.Value, "^/api/", RegexOptions.IgnoreCase);
                if (isApi)
                {
                    context.Response.ContentType = "application/json";
                    var json = new { Message = $"攔截到例外產生: {ex.Message}" };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(json));
                    return;
                }
                //context.Response.Redirect("/error");

                //await context.Response
                //    .WriteAsync($"{GetType().Name} 攔截到例外產生: {ex.Message}");
            }
        }
    }
}
