using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;
//using LibForCore.Encrypt;
using System.IO;

namespace LibForCore.ConfigJson
{
    /// <summary>
    /// 實際實作解密json資料區段
    /// 需定義有哪些資料需解密
    /// </summary>
    public class CustomConfigProvider : JsonConfigurationProvider
    {
        
        

        public CustomConfigProvider(CustJsonConfigurationSource source) : base(source)
        {
        }

        public override void Load(Stream stream)
        {
            // Let the base class do the heavy lifting.
            base.Load(stream);

           
            

            // But you have to make your own MyEncryptionLibrary, not included here
        }
    }

    public class CustJsonConfigurationSource : JsonConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new CustomConfigProvider(this);
        }
    }
}
