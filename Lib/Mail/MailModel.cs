namespace LibForCore.MailHelp
{
    /// <summary>
    /// 發 Mial 時需要的參數
    /// </summary>
    public class MailModel
    {
             
            /// <summary>
            /// Mail 主旨.
            /// </summary>
            public string subject { get; set; }

            /// <summary>
            /// Mail 內容
            /// </summary>
            public string body { get; set; }

            /// <summary>
            /// SMTP
            /// </summary>
            public string SMTP { get; set; }

            /// <summary>
            /// PORT
            /// </summary>
            public int PORT { get; set; }

            /// <summary>
            /// 是否使用 SSL
            /// </summary>
            public bool IsSSL { get; set; }

            /// <summary>
            /// 寄送方 Mail 帳號
            /// </summary>
            public string MailFrom { get; set; }

            /// <summary>
            /// 寄送方 Mail 密碼
            /// </summary>
            public string Password { get; set; }

            /// <summary>
            /// 收信方 Mail (多個帳號間用,隔開)
            /// </summary>
            public string MailTo { get; set; }
       
    }
}
