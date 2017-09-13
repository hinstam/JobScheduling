using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;

namespace JobScheduling.Web.Models
{
    public class ReportM
    {
        public string ReportPath { get; set; }

        public List<ReportParameter> Parameters { get; set; }

        public ReportDataSource DataSource { get; set; }

        public List<ReportDataSource> DataSourceList { get; set; }
    }



}