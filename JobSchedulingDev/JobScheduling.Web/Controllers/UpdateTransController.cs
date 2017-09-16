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
    public class UpdateTransController : CCASController
    {
        //
        // GET: /UpdateTrans/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ImportBinFileM Model)
        {
            UpdateTransBL update = new UpdateTransBL();
            UpdateTransM model = update.UpdateTrans();
            return View(model);
        }
    }
}
