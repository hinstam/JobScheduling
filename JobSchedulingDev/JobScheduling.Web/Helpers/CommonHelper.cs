using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobScheduling.Web.Helpers
{
    public class CommonHelper
    {
        /// <summary>
        /// for projectincomebreakdowncontroller
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static List<SelectListItem> AmountDict2CurrencyList(Dictionary<string, decimal?> dict)
        {
            List<SelectListItem> currencyItemList = null;
            if (dict != null)
            {
                currencyItemList = new List<SelectListItem>();
                foreach (var key in dict.Keys)
                {
                    currencyItemList.Add(new SelectListItem() { Value = key, Text = key});
                }
            }
            else
            {
                currencyItemList = new List<SelectListItem>() { new SelectListItem() { Value = string.Empty, Text = "--------------" } };
            }
            return currencyItemList;
        }

        /// <summary>
        /// For the invoice no. format in AM system, there are totally 6 digits. The first two digits refer to year and the remaining digits refer to running. no.E.g.130001, 13 = year 2013 ,0001 = running no.
        /// </summary>
        /// <param name="lastInsertNo">lastInsertNo. for invoiceNo.</param>
        /// <returns></returns>
        public static string GenerateInvoiceNo(string lastInsertNo)
        {
            string invoiceNo;
            string runningNo;
            DateTime dTime = DateTime.Now;
            int yearNo = dTime.Year;
            string year = null; 
            int month =dTime.Month;
            int day = dTime.Day;

            if (month<7)
            {
                yearNo--;
            }
            year = yearNo.ToString().Substring(yearNo.ToString().Length - 2).PadLeft(2, '0');
            if (string.IsNullOrEmpty(lastInsertNo))
            {
                return year + "0001";
            }
            if (year!=lastInsertNo.ToString().Substring(0,2))
            {
                
            }
            if (month == 7 && day == 1)
            {
                invoiceNo =year + "0001";
            }
            else
            {
                int tempNo = Convert.ToInt32(lastInsertNo.ToString().Substring(2)) + 1;
                int tempNolength = tempNo.ToString().Length;
                runningNo = tempNo.ToString().PadLeft(tempNolength > 4 ? tempNolength : 4, '0');
                invoiceNo = year + runningNo;
            }
            return invoiceNo;
        }
    }
}