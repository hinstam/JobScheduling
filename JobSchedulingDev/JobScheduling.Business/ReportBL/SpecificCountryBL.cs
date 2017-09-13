using JobScheduling.DataAccess.ReportDA;
using JobScheduling.Model.ReportModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JobScheduling.Business.ReportBL
{
    public class SpecificCountryBL : Business
    {
        public List<SpecificCountryM> GetSpecificCountrySum(SpecificCountrySearchM model)
        {
            SpecificCountryDA specificCountryDA = null;
            List<SpecificCountryM> result=new List<SpecificCountryM>() ;
            try
            {
                specificCountryDA = new SpecificCountryDA();

                var data = specificCountryDA.GetSpecificCountrySum(model);
                result = specificCountryDA.TableToList(data);
                if (result == null)
                {
                    result = new List<SpecificCountryM>();
                    SpecificCountryM m = new SpecificCountryM();
                    m.Transactions = 0;
                    m.Amount = 0;
                    result.Add(m);
                }
            }
            finally
            {
                if (specificCountryDA != null)
                    specificCountryDA.CloseConnection();
            }

            return result;
        }

        public SpecificCountryM GetSum(SpecificCountrySearchM searchModel)
        {
            SpecificCountryDA specificCountryDA = null;
            
            SpecificCountryM model = new SpecificCountryM();
            DataTable data=new DataTable ();
            new SpecificCountryDA();
            try
            {
                specificCountryDA = new SpecificCountryDA();
                data = specificCountryDA.GetSum(searchModel);

                if (data != null && data.Rows.Count > 0)
                {
                    model.TotalAmount = Convert.ToDecimal(data.Rows[0]["TotalAmount"] == DBNull.Value ? 0 : data.Rows[0]["TotalAmount"]);
                    model.TotalTransaction = Convert.ToDecimal(data.Rows[0]["TotalTransaction"] ?? 0);
                }
                else
                {
                    model.TotalAmount = 0;
                    model.TotalTransaction = 0;
                }
            }
            finally
            {
                if (specificCountryDA != null)
                    specificCountryDA.CloseConnection();
            }

            return model;
        }

        public DataTable GetFirstLastDate()
        {
            SpecificCountryDA specificCountryDA = null;
            DataTable data = new DataTable();
            try
            {
                specificCountryDA = new SpecificCountryDA();
                data = specificCountryDA.GetFirstLastDate();
            }
            finally
            {
                if (specificCountryDA != null)
                    specificCountryDA.CloseConnection();
            }
            return data;
        }
    }
}
