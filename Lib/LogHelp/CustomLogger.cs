using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NL = NLog;
using System;
using System.IO;

namespace LibForCore.LogHelp
{
    public class CustomLogger<T> : ILogger<T>
    {
        //private readonly string loggerName;
        //private readonly CustLogProviderConfiguration loggerConfig;
        //private readonly NL.Logger logger;

        //public CustomLogger(string name, CustLogProviderConfiguration config)
        //{
        //    loggerName = name;
        //    loggerConfig = config;
        //    logger = NL.LogManager.GetLogger(name);
        //}

        private readonly ILogger _logger;

        public CustomLogger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            string Formatter(TState innserState, Exception innerException)
            {
                // additional logic goes here, in my case that was extracting additional information from custom exceptions
                var message = formatter(innserState, innerException) ?? string.Empty;
                return message + " additional stuff in here";
            }

            _logger.Log(logLevel, eventId, state, exception, Formatter);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return _logger.BeginScope(state);
        }


        #region <Trace

        /// <summary>
        /// Traces 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        public void Trace(string message)
        {
           
            if (_logger.IsEnabled(LogLevel.Trace))
            {
                _logger.LogTrace(message);
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

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation(message);
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

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug(message);
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

            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(message);
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

            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError(message);
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