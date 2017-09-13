using JobScheduling.Web.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobScheduling.Web.Extensions
{
    public class ReportConvert
    {
        /// <summary>
        /// convert reports into file
        /// </summary>
        /// <param name="reportviewer"></param>
        /// <param name="fileType">excel,word,pdf,image</param>
        /// <param name="mimeType">返回的文件类型</param>
        /// <returns></returns>
        public static byte[] Report2File(ReportViewer reportviewer, FileType fileType, out string mimeType)
        {
            Warning[] warnings;
            string[] streamids;
            string encoding;
            string extension;

            byte[] bytes = reportviewer.LocalReport.Render(
                fileType.ToString(), null, out mimeType, out encoding,
                out extension, out streamids, out warnings
            );

            return bytes;
        }
    }
}