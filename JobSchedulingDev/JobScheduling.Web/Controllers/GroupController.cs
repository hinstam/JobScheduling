using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobScheduling.Business.SecurityBL;
using JobScheduling.Entity.SecurityModel;
using JobScheduling.Model.CommModel;
using JobScheduling.Web.Helpers;

namespace JobScheduling.Web.Controllers
{
    public class GroupController : CCASController
    {
        private GroupBL _groupBLFalse, _groupBLTrue;

        protected GroupBL groupBL(bool isTrue = false)
        {
            if (isTrue)
                return _groupBLTrue = _groupBLTrue ?? new GroupBL();
            return _groupBLFalse = _groupBLFalse ?? new GroupBL();
        }


        public ActionResult List()
        {
            var ps = ViewHelper.GetPageSizes(0);

            var result = groupBL().List(string.Empty, string.Empty, string.Empty, Convert.ToInt32(ps.First().Value), 1);

            ViewBag.SizeList = ps;

            return View(result);
        }


        [HttpPost]
        public ActionResult List(string GroupID, string GroupName, string Description, int pageSize, int pageIndex)
        {
            var result = groupBL().List(GroupID.RPercent(), GroupName.RPercent(), Description.RPercent(), pageSize, pageIndex);

            return PartialView("_PartialPageList", result);
        }



        public ActionResult New()
        {
            return View(new GroupM());
        }


        [HttpPost]
        public ActionResult New(GroupM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = groupBL(true).New(model);

                    if (result.IsSuccess && result.Affected > 0)
                    {
                        return RedirectToAction("List", "Group");
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
            return View(model);
        }


        public ActionResult Edit(string GroupID)
        {
            var result = groupBL().GetGroup(GroupID);
            return View(result);
        }


        [HttpPost]
        public ActionResult Edit(GroupM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = groupBL(true).Edit(model);

                    if (result.IsSuccess && result.Affected > 0)
                    {
                        return RedirectToAction("List", "Group");
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
            return View(model);

        }


        [HttpPost]
        public ActionResult Delete(string GroupID)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = groupBL(true).Delete(GroupID);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            return Json(result);
        }


        public ActionResult Detail(string GroupID)
        {
            var result = groupBL().GetGroup(GroupID);
            return View(result);
        }



        public ActionResult UserGroup(string ID)
        {
            var result = groupBL().GetAllGroup(string.Empty);

            ViewBag.GID = ID;

            return View(result);
        }


        [HttpPost]
        public ActionResult GetAllGroup(string GroupName)
        {
            ResultModel<GroupM> result = new ResultModel<GroupM>();

            try
            {
                result.EntityList = groupBL().GetAllGroup(GroupName.RPercent());
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            return Json(result);
        }


        [HttpPost]
        public ActionResult AddUserGroup(string groupID, string userUIDs)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = groupBL(true).AddUserGroup(groupID, userUIDs);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            return Json(result);
        }


        [HttpPost]
        public ActionResult DelUserGroup(string groupID, string userUIDs)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = groupBL(true).DelUserGroup(groupID, userUIDs);
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
