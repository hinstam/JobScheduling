using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;

namespace JobScheduling.Common
{
    public static class ExcelHelper
    {
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dataTable">数据源</param>
        /// <param name="fileName">Excel文件</param>
        public static void Export(DataTable dataTable, string fileName)
        {
            if (dataTable == null) throw new ArgumentNullException("dataTable");
            if (String.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");


            if (!String.IsNullOrEmpty(Path.GetDirectoryName(fileName)))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    WriteToStream(fs, dataTable);
                    fs.Flush();
                }
            }
            else
            {
                var httpContext = HttpContext.Current;
                if (httpContext == null) return;

                if (httpContext.Request.UserAgent.IndexOf("MSIE", StringComparison.OrdinalIgnoreCase) > 0)
                    fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);

                httpContext.Response.ContentType = "application/ms-excel";
                httpContext.Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", fileName));
                httpContext.Response.Clear();

                using (MemoryStream ms = new MemoryStream())
                {
                    WriteToStream(ms, dataTable);
                    httpContext.Response.BinaryWrite(ms.GetBuffer());
                    httpContext.Response.Flush();
                }
                httpContext.Response.End();
            }
        }

        /// <summary>
        /// 读取Excel文件数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作簿名称（如为空，取第一个工作薄）</param>
        /// <param name="headRowIndex">列头</param>
        /// <param name="startRowIndex">数据开始行</param>
        /// <param name="dataRowCount">数据行数（如为空则取到最后一行）</param>
        /// <param name="keyColumnIndex">Key列（如果该列没数据则判断整行为无效数据）</param>
        /// <param name="startColumnIndex">数据开始列（如为空则取第一列）</param>
        /// <param name="endColumnIndex">数据列数（如为空则取最后一列）</param>
        /// <returns>数据（DataTable格式）</returns>
        public static DataTable LoadExcelData(FileStream file,int[] headRowIndex, int startRowIndex
                                                ,string sheetName=null,int? dataRowCount = null, int? keyColumnIndex = null
                                                , int? startColumnIndex = null, int? dataColumnCount = null)
        {
            IWorkbook workbook = WorkbookFactory.Create(file);
            ISheet sheet = !String.IsNullOrEmpty(sheetName) ? workbook.GetSheet(sheetName) : workbook.GetSheetAt(0);

            if (sheet == null)
                return null;

            DataTable dt = new DataTable();
            IRow headRow = sheet.GetRow(headRowIndex.Max());

            if (headRow == null)
                return null;

            int startColumnNum = startColumnIndex.HasValue ? startColumnIndex.Value : headRow.FirstCellNum;
            int endColumnCount = dataColumnCount.HasValue ? dataColumnCount.Value : headRow.LastCellNum - startColumnNum;
            IList<int> dataCellNumList = new List<int>();
            int iColumnIndex = startColumnNum;
            for (int i = 0; i < endColumnCount; i++, iColumnIndex++)
            {
                string strHeadText = string.Empty;
                foreach (int iIndex in headRowIndex)
                {
                    IRow tempHeadRow = sheet.GetRow(iIndex);
                    ICell tempHeadCell = tempHeadRow.Cells[iColumnIndex];
                    if (!string.IsNullOrEmpty(tempHeadCell.ToString()))
                    {
                        if (string.IsNullOrEmpty(strHeadText))
                        {
                            strHeadText = tempHeadCell.ToString();
                        }
                        else
                        {
                            strHeadText += ";#" + tempHeadCell.ToString();
                        }
                    }
                }

                if (string.IsNullOrEmpty(strHeadText))
                    continue;

                if (dt.Columns.Contains(strHeadText))
                {
                    strHeadText = strHeadText + "_" + iColumnIndex.ToString();
                }
                DataColumn dc = new DataColumn(strHeadText);
                dt.Columns.Add(dc);
                dataCellNumList.Add(iColumnIndex);
            }

            int endRowCount = dataRowCount.HasValue ? dataRowCount.Value - 1 : sheet.LastRowNum - startRowIndex;
            int iRowIndex = startRowIndex;
            for (int i = 0; i <= endRowCount; i++, iRowIndex++)
            {
                IRow row = sheet.GetRow(iRowIndex);
                if (row == null)
                    continue;

                DataRow dataRow = dt.NewRow();

                if (keyColumnIndex.HasValue)
                {
                    ICell keyCell = row.GetCell(keyColumnIndex.Value);
                    if (keyCell == null || string.IsNullOrEmpty(keyCell.ToString()))
                    {
                        continue;
                    }
                }
                int dataColumnIndex = 0;
                foreach (int iDataCellIndex in dataCellNumList)
                {
                    ICell cell = row.GetCell(iDataCellIndex);

                    if (cell == null)
                        dataRow[dataColumnIndex] = null;
                    else
                    {
                        switch (cell.CellType)
                        {
                            case CellType.Blank:
                                break;
                            case CellType.Boolean:
                                dataRow[dataColumnIndex] = cell.BooleanCellValue;
                                break;
                            case CellType.Error:
                                //dataRow[dataColumnIndex] = cell.ErrorCellValue;
                                break;
                            case CellType.Formula:
                                //dataRow[dataColumnIndex] = cell.CellFormula;
                                if (cell.CachedFormulaResultType == CellType.Numeric)
                                    dataRow[dataColumnIndex] = cell.NumericCellValue;
                                else
                                    dataRow[dataColumnIndex] = cell.StringCellValue;
                                break;
                            case CellType.Numeric:
                                //dataRow[dataColumnIndex] = cell.NumericCellValue;
                                dataRow[dataColumnIndex] = cell.ToString(); ;
                                break;
                            case CellType.String:
                                dataRow[dataColumnIndex] = cell.StringCellValue;
                                break;
                            case CellType.Unknown:
                                dataRow[dataColumnIndex] = cell.StringCellValue;
                                break;
                            default:
                                break;
                        }
                    }
                    dataColumnIndex++;
                }
                dt.Rows.Add(dataRow);
            }

            return dt;
        }

        /// <summary>
        /// 读取Excel文件数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="headRowIndex">列头</param>
        /// <param name="startRowIndex">数据开始行</param>
        /// <param name="dataRowCount">数据行数（如为空则取到最后一行）</param>
        /// <param name="keyColumnIndex">Key列（如果该列没数据则判断整行为无效数据）</param>
        /// <param name="startColumnIndex">数据开始列（如为空则取第一列）</param>
        /// <param name="endColumnIndex">数据列数（如为空则取最后一列）</param>
        /// <returns>数据（DataTable格式）</returns>
        public static DataTable LoadExcelData(string strPath, int[] headRowIndex, int startRowIndex
                                                ,string sheetName=null, int? dataRowCount = null, int? keyColumnIndex = null
                                                , int? startColumnIndex = null, int? dataColumnCount = null)
        {
            if (!File.Exists(strPath))
                return null;
            FileStream file = new FileStream(strPath, FileMode.Open, FileAccess.Read);
            DataTable dt = new DataTable();
            try
            {
                dt = ExcelHelper.LoadExcelData(file, headRowIndex, startRowIndex
                                                ,sheetName,dataRowCount, keyColumnIndex
                                                , startColumnIndex, dataColumnCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                file.Close();
                file.Dispose();
            }
            return dt;
        }

        #region Private

        private static void WriteToStream(Stream stream, DataTable dataTable)
        {
            int rowNum = 0, columnNum = 0;
            HSSFWorkbook workbook = new HSSFWorkbook();

            ISheet sheet = workbook.CreateSheet();
            ICell cell = null;

            //Column header style
            ICellStyle headerCellStyle = workbook.CreateCellStyle();
            headerCellStyle.VerticalAlignment = VerticalAlignment.Center;
            headerCellStyle.Alignment = HorizontalAlignment.Center;

            headerCellStyle.FillForegroundColor = HSSFColor.LightYellow.Index;
            headerCellStyle.FillPattern = FillPattern.SolidForeground;

            IFont headerFont = workbook.CreateFont();
            headerFont.Boldweight = (short)FontBoldWeight.Bold;
            headerCellStyle.SetFont(headerFont);

            #region Document info
            POIDocument document = workbook as POIDocument;
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "Sinopec";
            document.DocumentSummaryInformation = dsi;

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "Sinopec.Cloud";
            si.Author = "Blacktea";
            document.SummaryInformation = si;
            #endregion

            #region Create header
            IRow row = sheet.CreateRow(rowNum++);
            row.HeightInPoints = 20;
            foreach (DataColumn column in dataTable.Columns)
            {
                cell = row.CreateCell(columnNum);
                cell.CellStyle = headerCellStyle;
                sheet.SetColumnWidth(columnNum, 4000);
                cell.SetCellValue(column.ColumnName);
                columnNum++;
            }
            #endregion

            #region Create body
            object value = null;
            foreach (DataRow dr in dataTable.Rows)
            {
                row = sheet.CreateRow(rowNum++);
                columnNum = 0;

                foreach (DataColumn column in dataTable.Columns)
                {
                    cell = row.CreateCell(columnNum++);
                    value = dr[column];
                    if (value != null)
                        cell.SetCellValue(value.ToString());
                }
            }
            #endregion

            workbook.Write(stream);
        }

        #endregion
    }
}
