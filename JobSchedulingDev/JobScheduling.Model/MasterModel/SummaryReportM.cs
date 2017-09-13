using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Model.MasterModel
{
    public class SummaryReportByCountryM
    {
        public string Country { get; set; }
        public long NumberOfTrx { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalTransaction { get; set; }

    }
}
