using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JobScheduling.Common
{
    public class ConfigHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static string UploadFilePath 
        {
            get { return ConfigurationManager.AppSettings["UploadFilePath"].ToString(); }       
        }

        /// <summary>
        /// 
        /// </summary>
        public static string DownloadFilePath
        {
            get { return ConfigurationManager.AppSettings["DownloadFilePath"].ToString(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ExportTplsPath
        {
            get { return ConfigurationManager.AppSettings["ExportTplsPath"].ToString(); }
        }
    }
}
