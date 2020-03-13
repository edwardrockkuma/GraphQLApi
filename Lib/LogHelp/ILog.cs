using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace LibForCore.LogHelp
{
    public interface ILog
    {

        #region <Trace
        /// <summary>
        /// Traces 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        void Trace(string message);

        /// <summary>
        /// Traces 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        void Trace(object outputObject);

        /// <summary>
        /// Traces 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        void Trace(string message, params object[] args);

        /// <summary>
        /// Traces 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        void Trace(string message, object outputObject);

        #endregion

        #region <Info

        void Info(string message);

        /// <summary>
        /// Debugs 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        void Info(object outputObject);

        /// <summary>
        /// Debugs 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Debugs 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        void Info(string message, object outputObject);

        #endregion

        #region <Debug

        void Debug(string message);

        /// <summary>
        /// Debugs 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        void Debug(object outputObject);

        /// <summary>
        /// Debugs 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// Debugs 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        void Debug(string message, object outputObject);

        #endregion

        #region <Warn
        /// <summary>
        /// Warns 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        void Warn(string message);

        /// <summary>
        /// Warns 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        void Warn(object outputObject);

        /// <summary>
        /// Warns 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        void Warn(string message, params object[] args);

        /// <summary>
        /// Warns 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        void Warn(string message, object outputObject);

        #endregion

        #region <Error
        /// <summary>
        /// Errors 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        void Error(string message);

        /// <summary>
        /// Errors 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        void Error(object outputObject);

        /// <summary>
        /// Errors 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        void Error(string message, params object[] args);

        /// <summary>
        /// Errors 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        void Error(string message, object outputObject);

        #endregion
    }
}
