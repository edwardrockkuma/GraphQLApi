using System;
using System.Collections.Generic;
using System.Text;
using DataModel.SettingModel;
using LibForCore.ConfigJson;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace DAL
{
    public class AbstractBaseDAL : IDisposable
    {
        /// <summary>
        /// DB裡定義執行者為系統程式
        /// </summary>
        /// <returns></returns>
        protected readonly Guid SystemID = new Guid("D441548C-2CEE-E811-A8A9-480FCF308590");
        protected readonly string _dbConnectionString;
        private SqlConnection sqlConnection;

        protected SqlConnection SqlConnection
        {
            get
            {
                return sqlConnection;
            }
        }

        public AbstractBaseDAL(IOptions<AppSettingMain> config)
        {
            _dbConnectionString = config.Value.DBConnectionString;

            var conBuilder = new SqlConnectionStringBuilder(_dbConnectionString);
            conBuilder.Pooling = true;
            // 設定Retry次數 , 需要.net 4.5以上 , 要注意 選擇的值應該會讓下列等式成立：
            // Connection Timeout = ConnectRetryCount * ConnectionRetryInterval
            conBuilder.ConnectRetryCount = config.Value.DBRetry;
            conBuilder.ConnectRetryInterval = 10;  // seconds
            conBuilder.ConnectTimeout = 30;

            _dbConnectionString = conBuilder.ConnectionString;


        }



        public virtual void Dispose()
        {
            // Disconnect from Database...
        }
    }
}
