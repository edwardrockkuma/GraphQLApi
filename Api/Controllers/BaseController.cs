//using DataModel.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using LibForCore.LogHelp;
//using LibForCore.AjaxData;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using DataModel.Data;
using System.Diagnostics;

namespace CiCdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController:ControllerBase
    {
        //protected CustomLogger<BaseController> _log { get; set; }
        private ILog _log = new NLogger("BaseController");

       protected IBaseService _baseService { get; set; }

       protected IHttpContextAccessor _accessor {get;set;}

       public BaseController(IHostedService timerTaskService, ILogger log, IHttpContextAccessor accessor)
       {
           //_baseService = baseService;
           //_log = log as CustomLogger<BaseController>;

       }

       protected void CreateJsonResponse(HttpStatusCode statusCode, object JsonObj,out IActionResult result)
       {
           result = Ok(JsonObj);
           switch(statusCode)
           {
                case HttpStatusCode.BadRequest:
                
                    result = BadRequest(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                    break;
                case HttpStatusCode.InternalServerError:
                
                    result = StatusCode(500);
                    break;
                default:
                    result = Ok(JsonObj);
                    break; 
           }
           //return Ok(JsonObj);
       } 

       protected string GetUserIp()
       {
           return _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
       } 
    }
}