using System;
using System.Collections.Generic;
using System.Text;
using LibForCore.ConfigJson;
using Newtonsoft.Json;

namespace DataModel.SettingModel
{
    /// <summary>
    /// 以強型別承接appsettings.{env}.json
    /// </summary>
    public class AppSettingMain : AppSetting
    {

        /// <summary>
        /// 發送Skype警告訊息需要的群組
        /// </summary>        
        public string SkypeGroup { get; set; }

        /// <summary>
        /// DB發生連線失敗時的Retry次數
        /// </summary>
        public int DBRetry { get; set; }

        /// <summary>
        /// 呼叫不到Nginx取流量api時的Retry次數
        /// </summary>
        public int GetTrafficRetry { get; set; }

        /// <summary>
        /// ServerZone Wildcard規則使用的隨機數(避免Name重複的機制)
        /// </summary>
        public string WildcardRandName { get; set; }

        public string BaseTrafficPath { get; set; }

        /// <summary>
        /// Nginx上的流量檔案Time Stamp的間隔時間
        /// 需配合Nginx才能抓到正確檔案
        /// Ex: 201905210910 => 分鐘的部份
        /// </summary>
        public int NginxJsonInerval { get; set; }

        /// <summary>
        /// 向Nginx問流量檔案的time out時間
        /// 單位(秒)
        /// </summary>
        public int HttpClientTimeout { get; set; }

        /// <summary>
        /// 開啟這個設定只是為了想補一次資料不跑Timer迴圈就停止
        /// </summary>
        public bool ManualControl { get; set; }

        /// <summary>
        /// 存放本地端流量檔案的路徑
        /// </summary>
        public string LocalTrafficJsonPath { get; set; }

        /// <summary>
        /// DB裡SysValue的key
        /// </summary>
        //public List<string> SysKey { get; set; }
    }
}
