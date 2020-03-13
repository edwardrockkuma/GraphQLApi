using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibForCore.MailHelp
{
    public interface IMailSend
    {
        //
        Task<bool> Send(MailModel mailmodel);
    }
}
