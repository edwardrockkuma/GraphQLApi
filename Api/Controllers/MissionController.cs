using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibForCore.LogHelp;
//using LibForCore.
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using DataModel.Data;
using System.Net;
using Newtonsoft.Json;
using Main.Services;
using DataModel;
using DataModel.Data;
using CiCdApi.Controllers;

namespace Main.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class MissionController : BaseController
    {
        private readonly TimerTaskService _timerTaskService;
        private readonly CustomLogger<MissionController> _log;
        private IHttpContextAccessor _accessor;

        public MissionController(IHostedService timerTaskService, ILogger<MissionController> log, IHttpContextAccessor accessor)
        :base(timerTaskService, log,  accessor)
        {
            _timerTaskService = timerTaskService as TimerTaskService;
            _log = log as CustomLogger<MissionController>;
            _accessor = accessor;
        }

        /// <summary>
        /// Test Api Get
        /// </summary>
        /// <returns></returns>
        [HttpGet("~/api/Mission/MissionGet/{id}")]
        public IActionResult MissionGet(int id)
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            IActionResult result = null;
            if(id <= 0)
            {
                object defaultObj = new object();
                statusCode = HttpStatusCode.BadRequest;
                CreateJsonResponse(statusCode , defaultObj ,out result);
                _log.Warn($"");
                return result;
            }

            RegisterViewModel testAccount = new RegisterViewModel();
            testAccount.Email = "test@gmail.com";
            testAccount.Password = "123456";
            testAccount.ConfirmPassword = "123456";

            return Ok(testAccount);
        }


       
    }
}