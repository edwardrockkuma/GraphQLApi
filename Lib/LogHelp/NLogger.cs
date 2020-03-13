using Newtonsoft.Json;
using NLog;
using System;
using System.Diagnostics;

namespace LibForCore.LogHelp
{
    /// <summary>
    /// 主要專案要記得安裝 NLog Config套件
    /// </summary>
    public class NLogger: ILog
    {
        private Logger logger;

        public NLogger()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="className">目前要使用Log的Class名字</param>
        public NLogger(string className)
        {
            logger = LogManager.GetLogger(className);
        }

        #region <Trace

        /// <summary>
        /// Traces 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        public void Trace(string message)
        {

            if (logger.IsTraceEnabled)
            {
                logger.Trace(message);
            }

        }

        /// <summary>
        /// Traces 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        public void Trace(string message, params object[] args)
        {
            Trace(string.Format(message, args));
        }

        /// <summary>
        /// Traces 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        public void Trace(object outputObject)
        {
            Trace(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Traces 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        public void Trace(string message, object outputObject)
        {
            Trace(message + Environment.NewLine +
            JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        #endregion

        #region <Info

        /// <summary>
        /// Info 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        public void Info(string message)
        {

            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }

        }

        /// <summary>
        /// Info 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        public void Info(string message, params object[] args)
        {
            Info(string.Format(message, args));
        }

        /// <summary>
        /// Info 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        public void Info(object outputObject)
        {
            Info(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Info 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        public void Info(string message, object outputObject)
        {
            Info(message + Environment.NewLine +
            JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        #endregion

        #region <Debug

        /// <summary>
        /// Debugs 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        public void Debug(string message)
        {

            if (logger.IsDebugEnabled)
            {
                logger.Debug(message);
            }

        }

        /// <summary>
        /// Debugs 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        public void Debug(string message, params object[] args)
        {
            Debug(string.Format(message, args));
        }

        /// <summary>
        /// Debugs 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        public void Debug(object outputObject)
        {
            Debug(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Debugs 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        public void Debug(string message, object outputObject)
        {
            Debug(message + Environment.NewLine +
            JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        #endregion

        #region <Warn

        /// <summary>
        /// Warns 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        public void Warn(string message)
        {

            if (logger.IsWarnEnabled)
            {
                logger.Warn(message);
            }

        }

        /// <summary>
        /// Warns 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        public void Warn(string message, params object[] args)
        {
            Warn(string.Format(message, args));
        }

        /// <summary>
        /// Warns 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        public void Warn(object outputObject)
        {
            Warn(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Warns 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        public void Warn(string message, object outputObject)
        {
            Warn(message + Environment.NewLine +
            JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        #endregion

        #region <Error

        /// <summary>
        /// Errors 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        public void Error(string message)
        {

            if (logger.IsErrorEnabled)
            {
                logger.Error(message);
            }

        }

        /// <summary>
        /// Errors 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        public void Error(string message, params object[] args)
        {
            Error(string.Format(message, args));
        }

        /// <summary>
        /// Errors 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        public void Error(object outputObject)
        {
            Error(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Errors 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        public void Error(string message, object outputObject)
        {
            Error(message + Environment.NewLine +
            JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        #endregion
    }
}
