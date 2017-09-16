using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobScheduling.Business.MasterBL;
using JobScheduling.Model.MasterModel;
using System.IO;
using System.Configuration;
using JobScheduling.Common;

namespace JobScheduling.Web.Controllers
{
    public class ImportStoreFileController : CCASController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ImportStoreFileM Model)
        {
            ImportStoreFileBL importStoreFileBl = new ImportStoreFileBL();
            HttpPostedFileBase file = Request.Files["filename"];
            string ls_exportpath, ls_excel = "", filePath;
            string oleDbCon=string.Empty;
            int fileLength=file.FileName.Length;

            ls_exportpath = Model.ExportFilename;
            ImportStoreFileM model = new ImportStoreFileM();
            string temp = System.AppDomain.CurrentDomain.BaseDirectory;
            ls_exportpath = temp + "upload";
            if ((fileLength>5&&file.FileName.Substring(fileLength-4,4)=="xlsx")||(fileLength>4&&file.FileName.Substring(fileLength-3,3)=="xls"))
            {
                //oleDbCon = string.Format(ConfigurationManager.ConnectionStrings["XlsxOledbCon"],);


                filePath = Server.MapPath(ConfigurationManager.AppSettings["UploadFilePath"]);
                if (FileHelper.CreateDictionary(filePath))
                {
                    filePath += file.FileName;
                    file.SaveAs(filePath);

                    string webPath = Request.Url.Authority;
                    model = importStoreFileBl.SaveFileToDB(filePath, ls_exportpath, webPath);
                    System.IO.File.Delete(filePath);
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteData()
        {
            ImportStoreFileBL tbl = new ImportStoreFileBL();
            tbl.DeleteData();

            return View(true);
        }
    }
}
