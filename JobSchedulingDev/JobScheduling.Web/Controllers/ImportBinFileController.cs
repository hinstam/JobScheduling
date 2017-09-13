using JobScheduling.Business.MasterBL;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.MasterModel;
using JobScheduling.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobScheduling.Web.Controllers
{
    public class ImportBinFileController : CCASController
    {
        //
        // GET: /ImportBinFile/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ImportBinFileM Model)
        {
            ImportBinFileBL ImportBinFileBLWithoutTran = new ImportBinFileBL();
            HttpPostedFileBase file = Request.Files["filename"];
            string ls_exportpath, ls_excel = "", path;
            ls_exportpath = Model.ExportFilename;
            ImportBinFileM model = new ImportBinFileM();
            string temp = System.AppDomain.CurrentDomain.BaseDirectory;
            ls_exportpath = temp + "upload";

            path = Server.MapPath("/upload/");
            if (file.FileName.Substring(file.FileName.Length - 4, 4) == "xlsx")
            {
                ls_excel = "bin.xlsx";
                file.SaveAs(path + ls_excel);
            }
            if (file.FileName.Substring(file.FileName.Length - 3, 3) == "xls")
            {
                ls_excel = "bin.xls";
                file.SaveAs(path + ls_excel);
            }
            string webPath = Request.Url.Authority;
            model = ImportBinFileBLWithoutTran.SaveFileToDB(path + ls_excel, ls_exportpath, webPath);
            System.IO.File.Delete(path + ls_excel);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteData()
        {
            ImportBinFileBL tbl = new ImportBinFileBL();
            tbl.DeleteData();

            return View(true);
        }
    }
}
