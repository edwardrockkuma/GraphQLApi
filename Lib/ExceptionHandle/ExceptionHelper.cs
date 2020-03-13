using System;
using System.Collections.Generic;
using System.Text;
using LibForCore.LogHelp;

namespace LibForCore.ExceptionHandle
{
    /*
     使用範例:
            // Part 1 
            ExceptionHandleExecute(() =>
            {
	            string varA = "var A";
	            int intA = int.Parse(varA);
            },_log , "function名稱 - 發生例外");
			
            // Part 2
            int intA1 = ExceptionHandleExecute(() =>
            {
	            string varA = "var A";
	            return getMyIntvalue(varA);
            },_log , "function名稱 - 發生例外");
    */

    public class ExceptionHelper
    {
        // 包裝共用的 try catch 
        //沒有回傳值，用Action
        public static void ExceptionHandleExecute(Action executeCode , ILog log,string err)
        {
            try
            {
                executeCode.Invoke();
            }
            catch (Exception ex)
            {
                //共用的處理Exception .....
                log.Error($"{err}:{ex.ToString()}");
            }
        }

        //有回傳值，用Func<T>
        public static T ExceptionHandleExecute<T>(Func<T> executeCode, ILog log, string err)
        {
            try
            {
                return executeCode.Invoke();
            }
            catch (Exception ex)
            {
                //共用的處理Exception .....
                log.Error($"{err}:{ex.ToString()}");
                return default(T);
            }
        }
    }
}
