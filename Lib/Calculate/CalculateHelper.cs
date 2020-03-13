using System;
using System.Collections.Generic;
using System.Text;

namespace LibForCore.Calculate
{
    /// <summary>
    /// 計算相關幫手
    /// </summary>
    public class CalculateHelper
    {
        /// <summary>
        /// 轉換byte單位顯示
        /// 以decialmal處理保留小數點
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ConvertBytes(decimal num)
        {
            string[] sizes = { "Bytes", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (num >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                num = num / 1024;
            }
            return String.Format("{0:0.##} {1}", num, sizes[order]);
        }
    }
}
