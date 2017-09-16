using JobScheduling.Business.SecurityBL;
using JobScheduling.Model.CommModel;
using JobScheduling.Entity.SecurityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobScheduling.Entity.CommModel;
using JobScheduling.Web.Helpers;


namespace JobScheduling.Web.Controllers
{
    public class UserController : CCASController
    {


        private UserBL _userBLFalse, _userBLTrue;

        protected UserBL userBL(bool isTrue = false)
        {
            if (isTrue)
                return _userBLTrue = _userBLTrue ?? new UserBL();
            return _userBLFalse = _userBLFalse ?? new UserBL();
        }


        public ActionResult New() 
        {
            return View(new  UserM());
        }


        [HttpPost]
        public ActionResult New(UserM model)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    var result = userBL(true).New(model);

                    if (result.IsSuccess && result.Affected > 0)
                    {
                        return RedirectToAction("List", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorMessage", result.Exception);
                    }
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
            return View(model);
        }


        public ActionResult List()
        {
            var ps = ViewHelper.GetPageSizes(0);

            var result = userBL().List(string.Empty, string.Empty, Convert.ToInt32(ps.First().Value), 1);

            ViewBag.SizeList = ps;

            return View(result);
        }


        [HttpPost]
        public ActionResult List(string userID, string userName, int pageSize, int pageIndex) 
        {
            var result = userBL().List(userID.RPercent(), userName.RPercent(), pageSize, pageIndex);

            return PartialView("_PartialPageList", result);
        }


        public ActionResult Edit(string UserID)
        {
            var result = userBL().GetUser(UserID);

            return View(result);
        }


        [HttpPost]
        public ActionResult Edit(UserM model)
        {
            if (string.IsNullOrEmpty(model.Password) && string.IsNullOrEmpty(model.ConfirmPassword) )
            {
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var result = userBL(true).Edit(model);

                    if (result.IsSuccess && result.Affected > 0)
                    {
                        return RedirectToAction("List", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorMessage", result.Exception);
                    }
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
            return View(model);

        }


        [HttpPost]
        public ActionResult Delete(string UserID)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = userBL(true).Delete(UserID);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            return Json(result);
        }


        public ActionResult Detail(string UserID)
        {
            var result = userBL().GetUser(UserID);
            return View(result);
        }


        [HttpPost]
        public ActionResult GetAllUser(string GroupID, string UserName)
        {
            ResultModel<UserM> result = new ResultModel<UserM>();

            try
            {
                result.EntityList = userBL().GetAllUser(GroupID, UserName.RPercent());
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            return Json(result);
        }


        [HttpPost]
        public ActionResult GetSelectedUser(string GroupID, string UserName)
        {
            ResultModel<UserM> result = new ResultModel<UserM>();

            try
            {
                result.EntityList = userBL().GetGroupUser(GroupID, UserName.RPercent());
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            return Json(result);
        }





    }
}
