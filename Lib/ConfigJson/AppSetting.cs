using System;
using System.Collections.Generic;
using System.Text;

namespace LibForCore.ConfigJson
{
    /// <summary>
    /// 對應dotnet core appsetting.json
    /// [注意]物件定義必須完全吻合appsetting.json , ex:無法以string的property去接在appsetting.json裡的{}
    /// </summary>
    public class AppSetting
    {
        public LoggingObj Logging { get; set; }
        public string AllowedHosts { get; set; }
        public string DBConnectionString { get; set; }
        public string Secret { get; set; }
    }

    public class LoggingObj
    {
        public LogLevelObj LogLevel { get; set; }
    }

    public class LogLevelObj
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string System { get; set; }
        public string CDNAgentProject { get; set; }
    }
}
