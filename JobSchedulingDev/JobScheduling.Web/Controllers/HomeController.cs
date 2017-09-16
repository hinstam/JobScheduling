using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobScheduling.Business.SecurityBL;
using JobScheduling.Model.CommModel;
using JobScheduling.Entity.SecurityModel;

using JobScheduling.Web.Helpers;
using JobScheduling.Entity.CommModel;

namespace JobScheduling.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("login");
        }


        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            UserBL userBL = new UserBL();

            if (ModelState.IsValid)
            {
                try
                {
                    ResultModel rm = userBL.Login(model);

                    if (rm.IsSuccess)
                    {
                        //return RedirectToAction("Summary");
                        return RedirectToAction("Operation");
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorMessage", rm.Exception);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("ErrorMessage", e.Message);
                }
            }
            return View(model);
        }


        public ActionResult Logout()
        {
            new UserBL().Logout();
            return RedirectToAction("Login");
        }


        public ActionResult InputError()
        {
            ViewBag.ErrorMessage = "You enter invalid characters";
            return View("../Shared/Error");
        }


        public ActionResult Operation()
        {
            OperationBL opBL = new OperationBL();

            List<SelectListItem> operationList = opBL.GetOperation();
            if (operationList.Count > 1)
            {
                ViewData.Add("OperationSelect", operationList);
                return View();
            }
            else
                return RedirectToAction("Summary");
        }

        #region  Ben Template start


        public ActionResult List()
        {
            ViewBag.Message = "List";
            return View();
        }

        public ActionResult Summary()
        {
            ViewBag.Message = "Summary";
            return View();
        }

        public ActionResult EditForm()
        {
            ViewBag.Message = "EditForm";
            return View();
        }
        //public ActionResult UserGroup()
        //{
        //    ViewBag.Message = "UserGroup";
        //    return View();
        //}



        public ActionResult Welcome()
        {
            ViewBag.Message = "Welcome";
            return View();
        }

        public ActionResult SelectUser()
        {
            ViewBag.Message = "SelectUser";
            return View();
        }
        public ActionResult Sorry()
        {
            ViewBag.Message = "Sorry";
            return View();
        }

        public ActionResult testGroupAsset()
        {
            ViewBag.Message = "testGroupAsset";
            return View();
        }
        #endregion  Ben Template end

        [HttpPost]
        public ActionResult Operation(string operationCode, string operationName)
        {
            JobScheduling.Business.Business business = new JobScheduling.Business.Business();
            business.Operation = operationCode;
            business.OperationName = operationName;
                        
            return RedirectToAction("Summary");
        }
    }
}
