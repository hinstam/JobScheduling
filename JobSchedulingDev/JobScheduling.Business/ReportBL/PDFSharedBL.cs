using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Reporting.WebForms;

namespace JobScheduling.Business.ReportBL
{
    public class PDFSharedBL
    {


        /// <summary>
        /// 生成纵向二进制PDF
        /// </summary>
        /// <param name="LR"></param>
        /// <returns></returns>
        public static byte[] LocalReportToByteV(LocalReport LR)
        {
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension ;

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


        /// <summary>
        /// 生成横向二进制PDF
        /// </summary>
        /// <param name="LR"></param>
        /// <returns></returns>
        public static byte[] LocalReportToByteH(LocalReport LR)
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
                <PageWidth>29.7cm</PageWidth> 
                <PageHeight>21cm</PageHeight>
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


    }
}
