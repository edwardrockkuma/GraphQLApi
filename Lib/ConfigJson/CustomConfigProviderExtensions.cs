using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibForCore.ConfigJson
{
    /// <summary>
    /// 自行定義提供IConfigurationBuilder的延伸method
    /// 解密appsetting.json的接口
    /// </summary>
    public static class CustJsonConfigurationExtensions
    {
        public static IConfigurationBuilder AddDecryptJsonFile(this IConfigurationBuilder builder, string path, bool optional,
         bool reloadOnChange)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("File path must be a non-empty string.");
            }

            var source = new CustJsonConfigurationSource
            {
                FileProvider = null,
                Path = path,
                Optional = optional,
                ReloadOnChange = reloadOnChange
            };

            source.ResolveFileProvider();
            builder.Add(source);
            return builder;
        }
    }
}
