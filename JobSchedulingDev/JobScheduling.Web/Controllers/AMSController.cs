using EG.CCAS.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EG.CCAS.Web.Controllers
{
    [ModuleAuthorize]
    public class CCASController : Controller
    {
        public CCASController()
        {
            //initialize Controller
        }
    }
}
