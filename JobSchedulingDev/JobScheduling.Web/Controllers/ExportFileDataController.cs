using JobScheduling.Business.MasterBL;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.FileModel;
using JobScheduling.Model.MasterModel;
using JobScheduling.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobScheduling.Web.Controllers
{
    public class ExportFileDataController : CCASController
    {
        //
        // GET: /ExportFileData/
        private ExportFileDataBL _ExportFileDataBL;

        protected ExportFileDataBL ExportFileDataBL()
        {

            return _ExportFileDataBL = _ExportFileDataBL ?? new ExportFileDataBL();
        }


        public ActionResult List()
        {
            var ps = ViewHelper.GetPageSizes(0);

            var result = ExportFileDataBL().List(string.Empty, string.Empty, Convert.ToInt32(ps.First().Value), 1);

            ViewBag.SizeList = ps;

            return View(result);
        }

        [HttpPost]
        public ActionResult List(string ExportDate, string Kind, int pageSize, int pageIndex)
        {
            bool re = CheckData(ExportDate);

            if (!re)
                ExportDate = null;

                var result = ExportFileDataBL().List(ExportDate, Kind, pageSize, pageIndex);


            
            
            return PartialView("_PartialExportFileData", result);
        }
        public bool CheckData(string ExportDate)
        {
            bool result = true;
            string ls_time = "";
            if (ExportDate != null && ExportDate != "")
                try
                {
                    ls_time = ExportDate.Substring(0, 4) + "/" + ExportDate.Substring(4, 2) + "/" + ExportDate.Substring(6, 2);
                    DateTime datetime = DateTime.Parse(ls_time);
                    result = true;
                }
                catch
                {
                    result = false;
                    return result;
                }


            return result;
        }


    }
}
