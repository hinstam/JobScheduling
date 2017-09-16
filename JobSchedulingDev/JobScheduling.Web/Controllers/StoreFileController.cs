using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobScheduling.Business.FileBL;
using JobScheduling.Web.Helpers;
using JobScheduling.Model.FileModel;
using JobScheduling.Model.CommModel;


namespace JobScheduling.Web.Controllers
{
    public class StoreFileController : CCASController
    {
        //
        // GET: /StoreFile/

        private StoreFileBL _storeFileBL;

        protected StoreFileBL StoreFileBL()
        {

            return _storeFileBL = _storeFileBL ?? new StoreFileBL();
        }

        public ActionResult List()
        {
            var ps = ViewHelper.GetPageSizes(0);

            var result = StoreFileBL().List(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Convert.ToInt32(ps.First().Value), 1);

            ViewBag.SizeList = ps;
            //ViewData["districtlist"] = new SelectList(StoreFileBL().GetDistrictList(string.Empty), "DistrictID", "DistrictCode");
            return View(result);
        }

        [HttpPost]
        public ActionResult List(string storeCode,string districtCode,string countryCode,string storeFirstDescription,string districtDescription,string address, int pageSize, int pageIndex)
        {
            var result = StoreFileBL().List(storeCode, districtCode, countryCode, storeFirstDescription, districtDescription, address, pageSize, pageIndex);
            return PartialView("_PartialPageList", result);
        }

        //
        // GET: /StoreFile/Details/5

        public ActionResult Detail(string storecode)
        {
            StoreFileM model = StoreFileBL().GetStoreFileByID(storecode);
            return View(model);
        }

        //
        // GET: /StoreFile/Create

        public ActionResult New()
        {
            ViewData["districtlist"] = new SelectList(StoreFileBL().GetDistrictList(string.Empty), "DistrictID", "DistrictCode");
            return View();
        }

        //
        // POST: /StoreFile/Create

        [HttpPost]
        public ActionResult New(StoreFileM model)
        {
            ResultModel resultModel = null;
            if (ModelState.IsValid)
            {
                try
                {
                    model.StoreCode = model.StoreCode.Trim();
                    resultModel =StoreFileBL().NewStoreFile(model);
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
            ViewData["districtlist"] = new SelectList(StoreFileBL().GetDistrictList(string.Empty), "DistrictID", "DistrictCode");
            return View(model);
        }

        //
        // GET: /StoreFile/Edit/5

        public ActionResult Edit(string storeid)
        {
            StoreFileM model = StoreFileBL().GetStoreFileByID(storeid);
            ViewData["districtlist"] = new SelectList(StoreFileBL().GetDistrictList(string.Empty), "DistrictID", "DistrictCode",model.DistrictID);
            return View(model);
        }

        //
        // POST: /StoreFile/Edit/5

        [HttpPost]
        public ActionResult Edit(StoreFileM model)
        {
            ResultModel resultModel = null;
            if (ModelState.IsValid)
            {
                try
                {
                    resultModel = StoreFileBL().UpdateStoreFile(model);
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
            ViewData["districtlist"] = new SelectList(StoreFileBL().GetDistrictList(string.Empty), "DistrictID", "DistrictCode");
            return View(model);
        }

        //
        // GET: /StoreFile/Delete/5

        //
        // POST: /StoreFile/Delete/5

        [HttpPost]
        public ActionResult Delete(string code)
        {
            ResultModel resultModel = new ResultModel();
            try
            {
                resultModel =StoreFileBL().DeleteStoreFileByCode(code);
            }
            catch (Exception e)
            {
                resultModel.IsSuccess = false;
                resultModel.Exception = e.Message;
            }
            return Json(resultModel);
        }

        /// <summary>
        /// choice country get district list
        /// </summary>
        /// <param name="countryid"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetRelateDistrict(string countryid)
        {
            IList<StoreFileM> storeList = new List<StoreFileM>();
            try
            {
                storeList = StoreFileBL().GetDistrictList(countryid);

            }
            catch (Exception ex)
            {

            }
            return Json(storeList);
        }

        /// <summary>
        /// choice district get store list 
        /// </summary>
        /// <param name="district"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetRelateStore(string districtid)
        {
            IList<StoreFileM> storeList = new List<StoreFileM>();
            try {
                storeList = StoreFileBL().GetStoreDescriptionList(districtid);
                
            }
            catch (Exception ex)
            {

            }
            return Json(storeList);
        }
    }
}
