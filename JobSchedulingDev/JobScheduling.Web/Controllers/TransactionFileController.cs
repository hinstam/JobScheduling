using JobScheduling.Business.FileBL;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.FileModel;
using JobScheduling.Model.MasterModel;
using JobScheduling.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobScheduling.Web.Controllers
{
    public class TransactionFileController : CCASController
    {
        //
        // GET: /TransactionFile/

        private TransactionFileBL _TransactionFileBL;

        protected TransactionFileBL TransactionFileBL()
        {

            return _TransactionFileBL = _TransactionFileBL ?? new TransactionFileBL();
        }


        public ActionResult List()
        {
            var ps = ViewHelper.GetPageSizes(0);

            var result = TransactionFileBL().List(string.Empty, string.Empty, string.Empty, Convert.ToInt32(ps.First().Value), 1);

            ViewBag.SizeList = ps;

            return View(result);
        }


        [HttpPost]
        public ActionResult List(string StoreCode, string SerialNo, string DocumentNo, int pageSize, int pageIndex)
        {
            var result = TransactionFileBL().List(StoreCode, SerialNo, DocumentNo, pageSize, pageIndex);
            return PartialView("_PartialPageList", result);
        }

        public ActionResult Detail(string StoreCode, string SerialNo, string DocumentNo)
        {
            TransactionFileM model = TransactionFileBL().GetTransactionFileByCode(StoreCode, SerialNo, DocumentNo);
            return View(model);
        }

        //
        // GET: /TransactionFile/Create

        public ActionResult New()
        {
            return View();
        }

        //
        // POST: /TransactionFile/Create

        [HttpPost]
        public ActionResult New(TransactionFileM model)
        {
            ResultModel resultModel = null;
            if (ModelState.IsValid)
            {
                try
                {
                    //model.BIN = model.BIN.Trim();
                    resultModel = TransactionFileBL().NewTransactionFile(model);
                    if (resultModel.IsSuccess)
                    {
                        return RedirectToAction("list");
                    }
                    else
                    {
                        ModelState.AddModelError("errormessage", resultModel.Exception);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("errormessage", e.Message);
                }
            }
            return View(model);
        }

        //
        // GET: /TransactionFile/Edit/5

        public ActionResult Edit(string StoreCode, string SerialNo, string DocumentNo)
        {
            TransactionFileM model = TransactionFileBL().GetTransactionFileByCode(StoreCode, SerialNo, DocumentNo);
            return View(model);
        }

        //
        // POST: /TransactionFile/Edit/5

        [HttpPost]
        public ActionResult Edit(TransactionFileM model)
        {
            ResultModel resultModel = null;
            if (ModelState.IsValid)
            {
                try
                {
                    resultModel = TransactionFileBL().UpdateTransactionFile(model);
                    if (resultModel.IsSuccess)
                    {
                        return RedirectToAction("list");
                    }
                    else
                    {
                        ModelState.AddModelError("errormessage", resultModel.Exception);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("errormessage", e.Message);
                }
            }
            return View(model);
        }

        //
        // POST: /TransactionFile/Delete/5

        [HttpPost]
        public ActionResult Delete(string StoreCode, string SerialNo, string DocumentNo)
        {
            ResultModel resultModel = new ResultModel();
            try
            {
                resultModel = TransactionFileBL().DeleteTransactionFileByCode(StoreCode, SerialNo, DocumentNo);
            }
            catch (Exception e)
            {
                resultModel.IsSuccess = false;
                resultModel.Exception = e.Message;
            }
            return Json(resultModel);
        }
    }
}
