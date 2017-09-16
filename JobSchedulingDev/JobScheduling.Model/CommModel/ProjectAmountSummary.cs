using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Entity.CommModel
{
    /// <summary>
    /// project amount summary
    /// </summary>
    public class ProjectAmountSummary
    {
        public int uid { get; set; }
        public decimal? TotalAgreementAmount { get; set; }
        public decimal? TotalIncomeAmount { get; set; }
        public decimal? TotalIncomeBreakdownAmount { get; set; }

        public Dictionary<string, decimal?> IncomeBreakDownDict { get; set; }
        public Dictionary<string, decimal?> AgreementAmountDict { get; set; }
        public Dictionary<string, decimal?> IncomeAmountDict { get; set; }
        public Dictionary<string, decimal?> IncomeBalanceDict { get; set; }
        public Dictionary<string, decimal?> InvoiceAmountDict { get; set; }

    }
}
