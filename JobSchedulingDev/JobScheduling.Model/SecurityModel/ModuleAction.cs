using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Entity.SecurityModel
{
    public class ModuleAction
    {
        public static readonly string SHOW_NAME = System.Configuration.ConfigurationManager.AppSettings["Show"].ToString();

        public static readonly string LIST_NAME = System.Configuration.ConfigurationManager.AppSettings["List"].ToString();

        public static readonly string INDEX_NAME = System.Configuration.ConfigurationManager.AppSettings["Index"].ToString();

        public static readonly string NEW_NAME = System.Configuration.ConfigurationManager.AppSettings["New"].ToString();

        public static readonly string EDIT_NAME = System.Configuration.ConfigurationManager.AppSettings["Edit"].ToString();

        public static readonly string DELETE_NAME = System.Configuration.ConfigurationManager.AppSettings["Delete"].ToString();

        public static readonly string VIEW_NAME = System.Configuration.ConfigurationManager.AppSettings["Detail"].ToString();

    }
}
