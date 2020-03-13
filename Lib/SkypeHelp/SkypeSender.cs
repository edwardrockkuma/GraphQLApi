using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NLog;
using LibForCore.LogHelp;
using LibForCore.ConfigJson;

namespace LibForCore.SkypeHelp
{
    public class SkypeSender: ISkypeSender
    {
        private readonly ILog logger = new NLogger("SkypeSender");

        /// <summary>
        /// 要通知的Skype群組
        /// </summary>
        public string SKGroup { get; set; }

        /// <summary>
        /// 外部程式提供的Client Instance
        /// </summary>
        public HttpClient ClientOutside { get; set; }

        public SkypeSender()
        {
            
        }

        public void SetUp(HttpClient clientOutside , string skGroup)
        {
            ClientOutside = clientOutside;
            SKGroup = skGroup;
        }

        public async Task SendToGroup(string from, string Msg)
        {
            string result = "";
            HttpClient client = null;
            try
            {

                // 發送Ex: http://skptw1.ansonlink.com:8152/?from=TestAP1&to=19:a9eaf679f2954137987b4507257be29c@thread.skype&msg=test1

                
                if(ClientOutside == null)
                {
                    client = new HttpClient();
                }
                else
                {
                    client = this.ClientOutside;
                }
            
                string Url = "http://skptw1.ansonlink.com:8152/";
                string group = this.SKGroup;
                
                var values = new Dictionary<string, string>()
                {
                    {"from", from},
                    {"to", group},
                    {"msg" ,  Msg}
                };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(Url, content).ConfigureAwait(false);
                //var response = client.SendAsync(new HttpRequestMessage(HttpMethod.Post ,Url));

                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    logger.Info("result: {0} , statuscode:{1}", result, response.StatusCode);
                }
                else
                {
                    logger.Error("{0} , StatusCode:{1}", "連線Skype Server失敗", response.StatusCode);
                }

                client.Dispose();

            }
            catch (Exception)
            {

                client.Dispose();
            }
            //return result;
        }
    }
}
