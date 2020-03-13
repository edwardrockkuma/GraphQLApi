
using System;
using System.Collections.Generic;
using System.Text;
using DataModel;

namespace BLL.Interface
{
    public interface IBaseService
    {
        /// <summary>
        /// 主要為了可以將IPV6的local ip 轉為ipv4
        /// </summary>
        /// <param name="originIP"></param>
        /// <returns></returns>
        string HandleIP(string originIP);

        
    }
}
