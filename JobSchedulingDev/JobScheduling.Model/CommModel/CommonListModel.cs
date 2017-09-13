using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace JobScheduling.Entity.CommModel
{
    public class CommonListModel
    {
        public PagingModel page;

        public string Viewtitle;

        public List<SelectListItem> Viewps;

        public string URL;

        public string Title;
    }
}
