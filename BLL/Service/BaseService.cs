using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interface;
using BLL.Interface;
using LibForCore.LogHelp;
using LibForCore.Database;
using System.Data;
using LibForCore.DataMapping;
//using LibForCore.Encrypt;
using DataModel;

namespace BLL.Service
{
    public class BaseService : IBaseService
    {
        private readonly ILog _log = new NLogger("BaseService");
        private readonly IBaseRepository _BaseRepo;
        public BaseService(IBaseRepository BaseRepo)
        {
            _BaseRepo = BaseRepo;
            _log.Debug("service created");
        }

        #region 本地檔案處理    


        #endregion

        public string HandleIP(string originIP)
        {
            string finalIP = "";
            // 特例處理IPv6
            if (originIP == "::1")
            {
                finalIP = "127.0.0.1";
            }
            else
            {
                finalIP = originIP;
            }
            return finalIP;
        }

        
    }
}
