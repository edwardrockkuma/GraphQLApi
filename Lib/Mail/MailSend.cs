using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using NLog;


namespace LibForCore.MailHelp
{
    public class MailSend : IMailSend
    {
        private Logger logger;

        public MailSend()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<bool> Send(MailModel mailmodel)
        {
            bool result = false;

            //主旨
            string subject = mailmodel.subject;

            //Mail 內容
            string body = mailmodel.body;
            //換行
            body = body.Replace("\\r\\n", Environment.NewLine);
            body = body.Replace("\\n", Environment.NewLine);

            //設定smtp主機
            string smtpAddress = mailmodel.SMTP;

            //設定Port
            int iPORT = mailmodel.PORT;

            //是否使用SSL
            bool bSSL = mailmodel.IsSSL;

            //填入寄送方email和密碼.
            string sMailFrom = mailmodel.MailFrom;
            string sPassword = mailmodel.Password;

            //收信方email
            string MailTo = mailmodel.MailTo;

            //嘗試發送次數
            int iCount = 3;

            for (int i = 0; i < iCount; i++)
            {
                try
                {
                    //若寄信 mail 沒有值就離開
                    if (string.IsNullOrEmpty(sMailFrom.Trim()) == true)
                    {
                        return false;
                    }
                    //若寄信 mail 密碼沒有值就離開
                    else if (string.IsNullOrEmpty(sPassword.Trim()) == true)
                    {
                        return false;
                    }
                    //若收信 mail 沒有值就離開
                    else if (string.IsNullOrEmpty(MailTo.Trim()) == true)
                    {
                        return false;
                    }

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(sMailFrom);
                        mail.BodyEncoding = System.Text.Encoding.UTF8;
                        mail.To.Add(MailTo);
                        mail.Subject = subject;
                        mail.Body = body;

                        // 若你的內容是HTML格式，則為True
                        mail.IsBodyHtml = false;

                        //夾帶檔案
                        //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                        //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, iPORT))
                        {
                            smtp.Credentials = new NetworkCredential(sMailFrom, sPassword);
                            smtp.EnableSsl = bSSL;
                            smtp.Send(mail);
                        }
                        
                    }

                    result = true;

                    break;
                }
                catch (Exception ex)
                {
                    //NLOG
                    logger.Warn("Mail 發送失敗: " + Environment.NewLine + ex.ToString());

                    await Task.Delay(1000);
                }
            }
            

            return result;
        }
    }
}
