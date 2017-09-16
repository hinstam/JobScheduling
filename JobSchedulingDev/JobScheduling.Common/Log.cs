using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Common
{
    public class Log
    {
        private static ILog _log = null;

        private static ILog Logger
        {
            get
            {
                if (_log == null)
                    _log = LogManager.GetLogger("LOG");

                return _log;
            }
        }

        /// <summary>
        /// DEBUG LOG
        /// </summary>
        /// <param name="module">Module Name</param>
        /// <param name="message">DEBUG Message</param>
        public static void Log_DEBUG(string module, string message)
        {
            if (Logger.IsDebugEnabled)
                Logger.Debug("[DEBUG]Module:" + module + " Message:" + message);
        }

        /// <summary>
        /// DEBUG LOG
        /// </summary>
        /// <param name="module">Module Name</param>
        /// <param name="message">LOG Message</param>
        public static void Log_DEBUG(Type module, string message)
        {
            if (Logger.IsDebugEnabled)
                Logger.Debug("[DEBUG]Type:" + module.FullName + " Message:" + message);
        }


        /// <summary>
        /// Info LOG
        /// </summary>
        /// <param name="module">Module Name</param>
        /// <param name="message">INFO Message</param>
        public static void Log_Info(string module, string message)
        {
            if (Logger.IsInfoEnabled)
                Logger.Info("[INFO]Module:" + module + " Message:" + message);
        }

        /// <summary>
        /// Info LOG
        /// </summary>
        /// <param name="module">Module Name</param>
        /// <param name="message">INFO Message</param>
        public static void Log_Info(Type module, string message)
        {
            if (Logger.IsInfoEnabled)
                Logger.Info("[INFO]Type:" + module.FullName + " Message:" + message);
        }
    
        /// <summary>
        /// ERROR LOG
        /// </summary>
        /// <param name="module">Module Name</param>
        /// <param name="ex">Exception</param>
        public static void Log_ERROR(Type module, Exception ex)
        {
            if (Logger.IsErrorEnabled)
                Logger.Error("[ERROR]Type:" + module.FullName + " Exception:" + ex.Message + "\r\nStackTrace:" + ex.StackTrace);
        }

        /// <summary>
        /// ERROR LOG
        /// </summary>
        /// <param name="module">Module Name</param>
        /// <param name="ex">Exception</param>
        public static void Log_ERROR(string module, Exception ex)
        {
            if (Logger.IsErrorEnabled)
                Logger.Error("[ERROR]Module:" + module + " Exception:" + ex.Message + "\r\nStackTrace:" + ex.StackTrace);
        }

        /// <summary>
        /// Warn LOG
        /// </summary>
        /// <param name="module">Module Name</param>
        /// <param name="ex">Exception</param>
        public static void Log_Warn(Type module, Exception ex)
        {
            if (Logger.IsWarnEnabled)
                Logger.Warn("[WARN]Type:" + module.FullName + " Exception:" + ex.Message + "\r\nStackTrace:" + ex.StackTrace);
        }

        /// <summary>
        /// Warn LOG
        /// </summary>
        /// <param name="module">Module Name</param>
        /// <param name="ex">Exception</param>
        public static void Log_Warn(string module, Exception ex)
        {
            if (Logger.IsWarnEnabled)
                Logger.Warn("[WARN]Module:" + module + " Exception:" + ex.Message + "\r\nStackTrace:" + ex.StackTrace);
        }

        /// <summary>
        /// Fatal LOG
        /// </summary>
        /// <param name="module">Module Name</param>
        /// <param name="ex">Exception</param>
        public static void Log_Fatal(Type module, Exception ex)
        {
            if (Logger.IsFatalEnabled)
                Logger.Fatal("[FATAL]Type:" + module.FullName + " Exception:" + ex.Message + "\r\nStackTrace:" + ex.StackTrace);
        }

        /// <summary>
        /// Warn LOG
        /// </summary>
        /// <param name="module">Module Name</param>
        /// <param name="ex">Exception</param>
        public static void Log_Fatal(string module, Exception ex)
        {
            if (Logger.IsFatalEnabled)
                Logger.Fatal("[FATAL]Module:" + module + " Exception:" + ex.Message + "\r\nStackTrace:" + ex.StackTrace);
        }
    }
}
