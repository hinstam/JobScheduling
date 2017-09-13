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
    public class ImportController : CCASController
    {
        //
        // GET: /Import/

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(ImportTransactionM Model)
        {
            ImportTransactionBL ImportTransactionBLWithoutTran = new ImportTransactionBL();
            HttpPostedFileBase file = Request.Files["filename"];
            string ls_exportpath, ls_excel="", path;
            ImportTransactionM model = new ImportTransactionM();
            string temp = System.AppDomain.CurrentDomain.BaseDirectory;
            ls_exportpath = temp + "upload";

            path = Server.MapPath("/upload/");
            if (file.FileName.Substring(file.FileName.Length - 4, 4) == "xlsx")
            {
                ls_excel = "trans3.xlsx";
                file.SaveAs(path + ls_excel);
            }
            if (file.FileName.Substring(file.FileName.Length - 3, 3) == "xls")
            {
                ls_excel = "trans3.xls";
                file.SaveAs(path + ls_excel);
            }
            
           // file.SaveAs(path);
            string webPath = Request.Url.Authority;
            model = ImportTransactionBLWithoutTran.SaveFileToDB(path + ls_excel, ls_exportpath, webPath);
            System.IO.File.Delete(path + ls_excel);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteData()
        {
            ImportTransactionBL tbl = new ImportTransactionBL();
            tbl.DeleteData();

            return View(true);
        }

    }
}
