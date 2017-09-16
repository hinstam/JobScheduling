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
    public class BinFileController : CCASController
    {
        //
        // GET: /BinFile/

        private BinFileBL _binFileBL;

        protected BinFileBL BinFileBL()
        {

            return _binFileBL = _binFileBL ?? new BinFileBL();
        }


        public ActionResult List()
        {
            var ps = ViewHelper.GetPageSizes(0);

            var result = BinFileBL().List(string.Empty, Convert.ToInt32(ps.First().Value), 1);

            ViewBag.SizeList = ps;

            return View(result);
        }


        [HttpPost]
        public ActionResult List(string code, int pageSize, int pageIndex)
        {
            var result = BinFileBL().List(code.RPercent(), pageSize, pageIndex);
            return PartialView("_PartialPageList", result);
        }

        public ActionResult Detail(string code)
        {
            BinFileM model = BinFileBL().GetBinFileByCode(code);
            return View(model);
        }

        //
        // GET: /BinFile/Create

        public ActionResult New()
        {
            return View();
        }

        //
        // POST: /BinFile/Create

        [HttpPost]
        public ActionResult New(BinFileM model)
        {
            ResultModel resultModel = null;
            if (ModelState.IsValid)
            {
                try
                {
                    model.BIN = model.BIN.Trim();
                    resultModel = BinFileBL().NewBinFile(model);
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
        // GET: /BinFile/Edit/5

        public ActionResult Edit(string code)
        {
            BinFileM model = BinFileBL().GetBinFileByCode(code);
            return View(model);
        }

        //
        // POST: /BinFile/Edit/5

        [HttpPost]
        public ActionResult Edit(BinFileM model)
        {
            ResultModel resultModel = null;
            if (ModelState.IsValid)
            {
                try
                {
                    resultModel = BinFileBL().UpdateBinFile(model);
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
        // POST: /BinFile/Delete/5

        [HttpPost]
        public ActionResult Delete(string code)
        {
            ResultModel resultModel = new ResultModel();
            try
            {
                resultModel = BinFileBL().DeleteBinFileByCode(code);
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
