using JobScheduling.Business.MasterBL;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.MasterModel;
using JobScheduling.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobScheduling.Web.Controllers
{
    public class CountryController : CCASController
    {
        //
        // GET: /Country/

        private CountryBL _countryBL;

        protected CountryBL countryBL()
        {

            return _countryBL = _countryBL ?? new CountryBL();
        }

        public ActionResult List()
        {
            var ps = ViewHelper.GetPageSizes(0);

            var result = countryBL().List(string.Empty, string.Empty, Convert.ToInt32(ps.First().Value), 1);

            ViewBag.SizeList = ps;

            return View(result);
        }


        [HttpPost]
        public ActionResult List(string code, string description, int pageSize, int pageIndex)
        {
            var result = countryBL().List(code.RPercent(), description.RPercent(), pageSize, pageIndex);

            return PartialView("_PartialPageList", result);
        }

        //
        // GET: /Country/Details/5

        public ActionResult Detail(string code)
        {
            var result = countryBL().GetCountry(code);
            return View(result);
        }

        //
        // GET: /Country/Create

        public ActionResult New()
        {
            return View(new CountryM());
        }

        //
        // POST: /Country/Create

        [HttpPost]
        public ActionResult New(CountryM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = countryBL().New(model);

                    if (result.IsSuccess && result.Affected > 0)
                    {
                        return RedirectToAction("List", "Country");
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

        //
        // GET: /Country/Edit/5

        public ActionResult Edit(string code)
        {
            var result = countryBL().GetCountry(code);

            return View(result);
        }

        //
        // POST: /Country/Edit/5

        [HttpPost]
        public ActionResult Edit(CountryM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = countryBL().Edit(model);

                    if (result.IsSuccess && result.Affected > 0)
                    {
                        return RedirectToAction("List", "Country");
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

        //
        // POST: /Country/Delete/5

        [HttpPost]
        public ActionResult Delete(string code)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = countryBL().Delete(code);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            return Json(result);
        }

        //
        // GET: /Country/UploadCountry/

        public ActionResult Upload()
        {
            return View();
        }

        //
        // POST: /Country/UploadCountry/

        [HttpPost]
        public ActionResult UploadCountry(CountryM Model)
        {
            CountryBL country = new CountryBL();
            HttpPostedFileBase file = Request.Files["filename"];
            string path = "",ls_excel="",ls_exportpath;
            ls_exportpath = Model.ExportFilename;
            CountryM model=new CountryM ();
            string temp = System.AppDomain.CurrentDomain.BaseDirectory;
            ls_exportpath = temp + "upload";

            path = Server.MapPath("/upload/");
            if (file.FileName.Substring(file.FileName.Length - 4, 4) == "xlsx")
            {
                ls_excel = "country.xlsx";
                file.SaveAs(path+ls_excel);
            }
            if (file.FileName.Substring(file.FileName.Length - 3, 3) == "xls")
            {
                ls_excel = "country.xls";
                file.SaveAs(path + ls_excel);
            }
            string webPath = Request.Url.Authority;
            model = country.SaveFileToDB(path + ls_excel, ls_exportpath, webPath);
            System.IO.File.Delete(path + ls_excel);
            //return File(model.ExportFilename, "application/excel", "file.xlsx");
            return View("Upload", model);
        }

        [HttpPost]
        public ActionResult DeleteData()
        {
            CountryBL tbl = new CountryBL();
            tbl.DeleteData();

            return View(true);
        }

        //[HttpPost]
        //public ActionResult GetAllCountry(string countryCode)
        //{
        //    ResultModel<string> result = new ResultModel<string>();

        //    try
        //    {
        //        CountryBL countryBL = new CountryBL();
        //        var list = countryBL.GetAllCountry(countryCode);
        //        result.EntityList = list;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.IsSuccess = false;
        //        result.Exception = ex.Message;
        //    }
        //    return Json(result);
        //}
    }
}
