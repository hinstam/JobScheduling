using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobScheduling.Web.Extensions
{
    public class DeleteFileAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Flush();
            var filePathResult = filterContext.Result as FilePathResult;
            if (filePathResult != null)
            {
                System.IO.File.Delete(filePathResult.FileName);
            }
        }
    }
}