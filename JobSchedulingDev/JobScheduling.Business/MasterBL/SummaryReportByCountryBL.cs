using JobScheduling.DataAccess.MasterDA;
using JobScheduling.Entity.CommModel;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.MasterModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Reporting.WebForms;

namespace JobScheduling.Business.MasterBL
{
    public class SummaryReportByCountryBL : Business
    {
        public virtual List<SummaryReportByCountryM> GetComparison(PamReportM model)
        {
            DataTable dt = new DataTable();
            SummaryReportByCountryDA sbyDa = null;
            List<SummaryReportByCountryM> comparison = new List<SummaryReportByCountryM>();
            try
            {
                sbyDa = new SummaryReportByCountryDA();
                dt = sbyDa.GetSummary(model);
                //if (dt.Rows.Count == 0) {
                //    ProjectPLSummaryM pl = new ProjectPLSummaryM();
                //    pl.ActualArtistFee = 0;
                //    return new List<ProjectPLSummaryM>() { new ProjectPLSummaryM() { } };
                //}
                comparison = sbyDa.Table2List(dt);
            }
            finally
            {
                if (sbyDa != null)
                    sbyDa.CloseConnection();
            }
            return comparison;
        }

        public SummaryReportByCountryM GetTotalSum(PamReportM searchModel)
        {
            SummaryReportByCountryDA sbyDa = null; 
            SummaryReportByCountryM model = new SummaryReportByCountryM();
            DataTable data = new DataTable();
            try
            {
                sbyDa = new SummaryReportByCountryDA();
                data = sbyDa.GetTotalSum(searchModel);

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
                if (sbyDa != null)
                    sbyDa.CloseConnection();
            }
            return model;
        }


        public static byte[] LocalReportToByteV(LocalReport LR)
        {
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            //The DeviceInfo settings should be changed based on the reportType
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
            string deviceInfo = string.Format(@"
            <DeviceInfo>
                <OutputFormat>PDF</OutputFormat>  
                <PageWidth>21cm</PageWidth> 
                <PageHeight>29.7cm</PageHeight>
                <MarginTop>0</MarginTop>
                <MarginLeft>0</MarginLeft>
                <MarginRight>0</MarginRight>
                <MarginBottom>0</MarginBottom>
            </DeviceInfo>");

            renderedBytes = LR.Render(reportType,
                        deviceInfo,
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);

            return renderedBytes;
        }

        public DataTable GetFirstLastDate()
        {
            SummaryReportByCountryDA specificCountryDA = null;
            DataTable data = new DataTable();
            try
            {
                specificCountryDA = new SummaryReportByCountryDA();
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
