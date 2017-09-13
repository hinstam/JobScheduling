using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobScheduling.Web.Helpers
{
    public static class ViewHelper
    {

        public static List<SelectListItem> GetPageSizes(int Default)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            foreach (int Point in new int[] {  20, 30, 40, 50 })
            {
                result.Add(new SelectListItem { Text = Point.ToString(), Value = Point.ToString(), Selected = Point == Default ? true : false });
            }
            return result;
        }
    }
}