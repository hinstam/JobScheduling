using JobScheduling.Business.SecurityBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobScheduling.Web.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Module/

        public ActionResult Index()
        {
            ModuleBL moduleBL = new ModuleBL();
            return View(moduleBL.GetMenu());
        }
    }
}
