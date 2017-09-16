using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobScheduling.Business.SecurityBL;
using JobScheduling.Entity.SecurityModel;

namespace JobScheduling.Web.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ChangePassword()
        {
            return View("../User/ChangePassword", new ChangePasswordModel());
        }


        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel cpModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = new UserBL().ChangePassword(cpModel.OldPassword, cpModel.NewPassword);

                    if (result.IsSuccess)
                        return RedirectToAction("List", "User");
                    else
                        ModelState.AddModelError("ErrorMessage", result.Exception);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ErrorMessage", ex.Message);
                }
            }
            else
            {
                var e = ModelState.Where(z => z.Key == string.Empty && z.Value.Errors != null);

                if (e.Count() > 0)
                {
                    ModelState.AddModelError("ErrorMessage", e.First().Value.Errors.First().ErrorMessage);
                }
            }
            return View("../User/ChangePassword",cpModel);
        }





    }
}
