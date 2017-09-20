using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobScheduling.Web.Extensions;
using JobScheduling.Model.GanttSourceModel;

namespace JobScheduling.Web.Controllers
{
    public class ProductSchedulingController : Controller
    {
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
        public ActionResult GetProductSchedulings()
        {
            var ganttSources = new List<GanttSourceModel>();

            return new CamelJsonResult(ganttSources);

        }

    }
}
