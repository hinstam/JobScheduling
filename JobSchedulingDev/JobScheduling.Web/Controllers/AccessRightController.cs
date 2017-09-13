using JobScheduling.Business.SecurityBL;
using JobScheduling.Entity.SecurityModel;
using JobScheduling.Model.CommModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobScheduling.Web.Controllers
{
    public class AccessRightController : CCASController
    {
        //
        // GET: /AccessRight/
        private AccessRightBL _arBLNoTran;
        private AccessRightBL _arBLWithTran;

        protected AccessRightBL AccessRightBLNoTran
        {
            get
            {
                if (_arBLNoTran == null)
                    _arBLNoTran = new AccessRightBL();

                return _arBLNoTran;
            }
        }

        protected AccessRightBL AccessRightBLWithTran
        {
            get
            {
                if (_arBLWithTran == null)
                    _arBLWithTran = new AccessRightBL();

                return _arBLWithTran;
            }
        }

        public ActionResult Index(string id)
        {
            return View(AccessRightBLNoTran.GetAllAccessRight(id));
        }

        //
        // POST: /AccessRight/Index

        [HttpPost]
        public ActionResult Index(AccessRightModel arModel)
        {
            ResultModel result = new ResultModel();

            try
            {
                result.IsSuccess=AccessRightBLWithTran.EditModuleAccessRight(arModel.ModuleActions, arModel.SelectedGroupID);
            }
            catch (Exception e)
            {
                result.Exception = e.Message;
                result.IsSuccess = false;
            }
            return Json(result);
        }

        
    }
}
