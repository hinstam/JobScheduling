using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobScheduling.Common;
using System.IO;
using JobScheduling.Web.Extensions;

namespace JobScheduling.Web.Controllers
{
    public class FileController : Controller
    {
        private const string exportTimeFormat = "yyyyMMddhhmmss";

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [DeleteFile]
        public ActionResult Download(string fileName,int type)
        {
            var downloadDir = Server.MapPath(ConfigHelper.DownloadFilePath);

            var fileDownloadName=string.Empty;

            if (!Directory.Exists(downloadDir))
            {
                FileHelper.CreateDictionary(downloadDir);
            }

            var filePath= Path.Combine(downloadDir,fileName);

            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            FileInfo file=new FileInfo(filePath);

            var exportFileSuffix=  DateTimeHelper.GetFormatDateTime(DateTime.Now, exportTimeFormat)+file.Extension;

            switch(type)
            {
                case 1:
                    fileDownloadName = "生产计划表_" + exportFileSuffix;
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    fileDownloadName = "download_" + exportFileSuffix;
                    break;
            }

            return File(filePath, "application/vnd.ms-excel", fileDownloadName);
        }

    }
}
