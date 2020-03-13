using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using DAL.Interface;
using DataModel.SettingModel;
using LibForCore.ConfigJson;
using LibForCore.Database;
using LibForCore.LogHelp;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace DAL.Repository
{
    public class BaseRepository : AbstractBaseDAL, IBaseRepository
    {
        private readonly ILog log = new NLogger("BaseRepository");

        public BaseRepository(IOptions<AppSettingMain> config) : base(config)
        {
            log.Info($"{this._dbConnectionString}");
        }


        
    }
}
