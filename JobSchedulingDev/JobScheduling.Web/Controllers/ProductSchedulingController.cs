using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobScheduling.Web.Extensions;
using JobScheduling.Model.GanttSourceModel;
using JobScheduling.Model;
using JobScheduling.Common;
using JobScheduling.Business.SchedulingBL;
using System.IO;

namespace JobScheduling.Web.Controllers
{
    public class ProductSchedulingController : Controller
    {
        private const string productSchedulingTplsName="ProductScheduling";

        //
        // GET: /ProductScheduling/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProductSchedulingGantt()
        {
            var ganttSources = new List<GanttSourceModel>();

            return new CamelJsonResult(ganttSources);

        }

        /// <summary>
        /// 导出生产计划表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExportProductScheduling()
        {
            var result = new ExportResultModel();

            var exportTplsDir= Server.MapPath(ConfigHelper.ExportTplsPath);

            var downloadFileDir=Server.MapPath(ConfigHelper.DownloadFilePath);

            if (!Directory.Exists(exportTplsDir))
            {
                FileHelper.CreateDictionary(exportTplsDir);
            }

            if (!Directory.Exists(downloadFileDir))
            {
                FileHelper.CreateDictionary(downloadFileDir);
            }

            var guid=Guid.NewGuid().ToString();

            var tplsFilePath = Path.Combine(exportTplsDir, productSchedulingTplsName+".xlsx");

            var exportFileName=productSchedulingTplsName+"_"+guid+".xlsx";

            var downloadFilePath = Path.Combine(downloadFileDir, exportFileName);

            if (!System.IO.File.Exists(tplsFilePath))
            {
                result.Message = "excel模板文件不存在";
                return Json(result);
            }

            var productSchedulingBL = new ProductSchedulingBL();

            var isSave= productSchedulingBL.SaveExportProductScheduling(tplsFilePath,downloadFilePath);

            if (isSave)
            {
                result.FileName = exportFileName;
                result.IsSuccess = true;
            }

            return Json(result);
        }

    }
}
