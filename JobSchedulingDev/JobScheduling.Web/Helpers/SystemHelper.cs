using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Util;

namespace JobScheduling.Web.Helpers
{
    public class CustomRequestValidation : RequestValidator
    {
        public CustomRequestValidation() { }
        protected override bool IsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
        {
            //block script tags
            var idx = value.ToLower().IndexOf("<script");
            if (idx > -1)
            {
                validationFailureIndex = idx;
                context.Response.Redirect("~/Home/InputError");
                return false;
            }
            else
            {
                validationFailureIndex = 0;
                return true;
            }
        }
    }

    public static class SystemHelper
    {
        public static string RPercent(this string _context)
        {
            if (string.IsNullOrEmpty(_context))
                return "";
            else
                return _context.Replace("/","//").Replace("%", "/%");
        }

    }



}