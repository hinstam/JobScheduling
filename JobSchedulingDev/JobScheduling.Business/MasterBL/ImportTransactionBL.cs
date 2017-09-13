using JobScheduling.DataAccess.MasterDA;
using JobScheduling.Entity.CommModel;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.MasterModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace JobScheduling.Business.MasterBL
{
    public class ImportTransactionBL : Business
    {

        /// <summary>
        /// Get Bank by uid
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public virtual ImportTransactionM SaveFileToDB(string filenpath, string ls_exportpath, string webPath)
        {
            ImportTransactionM model = new ImportTransactionM();
            ImportTransactionDA importDA = null; 
            string ls_cretable, ls_sql, ls_store, ls_region = "", ls_trsdate = "";
            long ll_err_num = 0;
            OleDbConnection csv_cnn = new OleDbConnection();

            try
            {
                importDA = new ImportTransactionDA();
                if (filenpath.Substring(filenpath.Length - 4, 4) == "xlsx")
                {
                    csv_cnn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filenpath + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1';";
                }
                if (filenpath.Substring(filenpath.Length - 3, 3) == "xls")
                {
                    csv_cnn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filenpath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";
                }

                csv_cnn.Open();
                DataTable dtExcelSchema = csv_cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                string SheetName = "";
                for (int i = 0; i < dtExcelSchema.Rows.Count; i++)
                {
                    DataRow dr = dtExcelSchema.Rows[i];
                    // 用下面方法筛选无效的sheet
                    if (dr[2].ToString().Contains("$") && !dr[2].ToString().EndsWith("FilterDatabase"))
                    {
                        SheetName = dr["TABLE_NAME"].ToString();
                        break;
                    }
                }


                //string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                DataTable ldt_csv = new DataTable();
                OleDbDataAdapter lda_csv = new OleDbDataAdapter("SELECT *  FROM [" + SheetName + "]", csv_cnn);
                try
                {
                    lda_csv.Fill(ldt_csv);
                }
                catch (Exception e)
                {
                    model.Errmsg = "Can't find the sheet name in the current Excel File";
                    csv_cnn.Close();
                    importDA.CloseConnection();
                    return model;
                }
                //if (!ldt_csv.Columns.Contains("Store Code") || !ldt_csv.Columns.Contains("Transaction Date") || !ldt_csv.Columns.Contains("Document No#") || !ldt_csv.Columns.Contains("Credit Card No#") || !ldt_csv.Columns.Contains("Base amount") || !ldt_csv.Columns.Contains("BIN") || !ldt_csv.Columns.Contains("Serial No#"))
                //{
                //    model.Errmsg = "This is not a Transaction File,Please check it";
                //    csv_cnn.Close();
                //    importDA.CloseConnection();
                //    return model;
                //}
                /**********export*************/
                //Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application();
                //Microsoft.Office.Interop.Excel.Workbooks myBooks = myExcel.Workbooks;!ldt_csv.Columns.Contains("StoreCode") || !ldt_csv.Columns.Contains("TransactionDate") || !ldt_csv.Columns.Contains("DocumberNo.") || !ldt_csv.Columns.Contains("CreditCardNumber") || !ldt_csv.Columns.Contains("Baseamount") || !ldt_csv.Columns.Contains("BIN") || !ldt_csv.Columns.Contains("TransactionSerial")
                //object oMissing = System.Reflection.Missing.Value;
                ////Microsoft.Office.Interop.Excel.Workbook myBook = myBooks.Open(filenpath + "export.xlsx", oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
                //Microsoft.Office.Interop.Excel.Workbook workbook = myBooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                //Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets["sheet1"];

                //Microsoft.Office.Interop.Excel.Range range;

                //for (int i = 0; i < ldt_csv.Columns.Count; i++)
                //{
                //    worksheet.Cells[1, i + 1] = ldt_csv.Columns[i].ColumnName;
                //    range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                //    range.Interior.ColorIndex = 0;
                //    range.Font.Bold = true;
                //}

                /******************************/

                csv_cnn.Close();
                OleDbConnection csv_cnn2 = new OleDbConnection();
                string exportpath = DateTime.Now.ToFileTime().ToString();
                try
                {

                    string filepath = ls_exportpath + "\\" + exportpath;


                    if (!Directory.Exists(filepath))//判断文件夹是否已经存在
                    {
                        Directory.CreateDirectory(filepath);//创建文件夹
                    }

                    string exportFile = filepath + "\\ExportTransaction.xlsx";


                    csv_cnn2.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + exportFile + ";Extended Properties='Excel 12.0 Xml;HDR=YES'";

                    csv_cnn2.Open();
                    OleDbCommand cmd = new OleDbCommand("create table [sheet1]([Store Code] Text,[Transaction Date] Text,[Document No] Text,[SerialNo] Text,[Credit Card No] Text,[Base Amount] Text,[BIN] Text)", csv_cnn2);
                    cmd.ExecuteNonQuery();


                }
                catch (Exception er)
                {
                    model.Errmsg = "Error excel file create fail ";
                    csv_cnn2.Close();
                    importDA.CloseConnection();
                    return model;
                }

               
                ls_sql = @"insert into t_ccas_transaction_master(Region,SerialNo,StoreCode,TransactionDate,DocumentNo,CreditCardNo,BaseAmount,BIN,CreatedDate,LastUpdateDate,UpdatedBy) values
                           (@Region,@SerialNo,@StoreCode,@TransactionDate,@DocumentNo,@CreditCardNo,@BaseAmount,@BIN,@CreatedDate,@LastUpdateDate,@UpdatedBy)";

                string CreditCardNo = "";
                model.NumOfData = ldt_csv.Rows.Count;
                for (int i = 0; i < ldt_csv.Rows.Count; i++)
                {
                    try
                    {
                        ls_store = ldt_csv.Rows[i]["Store Code"].ToString();
                    }
                    catch
                    {
                        model.Errmsg = "Can't find the Column in the current Excel File,please check the excel file";
                        csv_cnn.Close();
                        importDA.CloseConnection();
                        return model;
                    }
                    if (ls_store.Substring(0, 1) == "1")
                        ls_region = "HK";
                    else if (ls_store.Substring(0, 1) == "2")
                        ls_region = "MO";
                    else if (ls_store.Substring(0, 1) == "6")
                        ls_region = "CN";
                    else if (ls_store.Substring(0, 1) == "7")
                        ls_region = "SG";


                    if (ldt_csv.Rows[i]["Credit Card Number"].ToString().Length > 20)
                    {
                        CreditCardNo = ldt_csv.Rows[i]["Credit Card Number"].ToString().Substring(0, 20);
                    }
                    else
                        CreditCardNo = ldt_csv.Rows[i]["Credit Card Number"].ToString();

                    //DateTime transDate;
                    try
                    {
                        ls_trsdate = DateTime.Parse(ldt_csv.Rows[i]["Transaction Date"].ToString()).ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    }
                    catch
                    {
                        ll_err_num++;
                        WriteErrorToExcel(csv_cnn2, ldt_csv.Rows[i]);
                        continue;
                    }

                    Dictionary<string, object> paramsValue = new Dictionary<string, object>() { 
                                                        {"Region",ls_region},
                                                        {"SerialNo",ldt_csv.Rows[i]["Transaction Serial"].ToString () },
                                                        {"StoreCode",ldt_csv.Rows[i]["Store Code"].ToString () },
                                                        {"TransactionDate",ls_trsdate},
                                                        { "DocumentNo", ldt_csv.Rows[i]["Documber No#"].ToString () },
                                                        { "CreditCardNo", CreditCardNo},
                                                        { "BaseAmount", ldt_csv.Rows[i]["Base amount"].ToString () },
                                                        { "BIN", ldt_csv.Rows[i]["BIN"].ToString () },
                                                        { "CreatedDate", DateTime.Now },
                                                        { "LastUpdateDate", DateTime.Now },
                                                        {"UpdatedBy","System"}};


                    try
                    {

                        int rel = importDA.InsertTable(ls_sql, paramsValue);

                        if (rel <= 0)
                        {
                            ll_err_num++;
                            //for (int k = 0; k < ldt_csv.Columns.Count; k++)
                            //{
                            //    worksheet.Cells[ll_err_num + 1, k + 1] = ldt_csv.Rows[i][k].ToString();
                            //}
                            WriteErrorToExcel(csv_cnn2, ldt_csv.Rows[i]);
                        }
                    }
                    catch (Exception e)
                    {
                        ll_err_num++;
                        //for (int k = 0; k < ldt_csv.Columns.Count; k++)
                        //{

                        //    worksheet.Cells[ll_err_num + 1, k + 1] = ldt_csv.Rows[i][k].ToString();
                        //}
                        WriteErrorToExcel(csv_cnn2, ldt_csv.Rows[i]);
                    }
                }
                //string exportpath = DateTime.Now.ToFileTime().ToString();
                //string filepath = ls_exportpath + "\\" + exportpath;
                //if (ll_err_num > 0)
                //{
                //    if (!Directory.Exists(filepath))//判断文件夹是否已经存在
                //    {
                //        Directory.CreateDirectory(filepath);//创建文件夹
                //    }
                //    workbook.SaveAs(filepath + "\\ExportTransaction.xlsx", oMissing, oMissing, oMissing, oMissing, oMissing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, oMissing, oMissing, oMissing, oMissing, oMissing);
                //}
                ////关闭文件  
                //workbook.Close(false, filepath + "\\ExportTransaction.xlsx", true);
                //myExcel.Quit();
                GC.Collect();
                model.ExportFilename = "http://" + webPath + "/upload/" + exportpath + "/ExportTransaction.xlsx";
                string insertsql = "insert into t_ccas_export_master values (@ExportDate,@Path,@Kind,@CreatedBy)";
                Dictionary<string, object> pv = new Dictionary<string, object>() { 
                                    {"ExportDate",DateTime.Now},
                                    {"Path",model.ExportFilename },
                                    { "Kind", "Transaction File" },
                                    {"CreatedBy",UserID}};

                int re = importDA.InsertTable(insertsql, pv);

                model.NumOfFail = ll_err_num;
                model.NumOfSuccess = model.NumOfData - ll_err_num;
                model.HasResult = true;
                csv_cnn2.Close();
            }
            catch (Exception ex)
            {
                model.Errmsg = ex.Message;
            }
            finally
            {
                if (importDA != null)
                    importDA.CloseConnection();
            }
            //int i = ImportTransactionDA.CreateTempTable(ls_cretable);


            // if (i != 0)



            //   return BankDA.RowToEntity(dr);
            //  else
            return model;
        }

        private void WriteErrorToExcel(OleDbConnection csv_cnn2, DataRow row)
        {
            string strSQL = "INSERT INTO [Sheet1] ([Store Code] ,[Transaction Date] ,[Document No],[SerialNo] ,[Credit Card No],[Base Amount],[BIN]) VALUES (?,?, ?,?,?,?,?)";

            OleDbCommand cmd = new OleDbCommand(strSQL, csv_cnn2);

            for (int i = 0; i < 7; i++)
            {
                cmd.Parameters.Add(i.ToString(), OleDbType.VarChar);
                cmd.Parameters[i].Value = row[i];
            }

            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }


        public void DeleteData()
        {
            ImportTransactionDA importDA = null;
            try
            {
                importDA = new ImportTransactionDA();
                string ls_sql = "truncate table t_ccas_transaction_master";
                int re = importDA.InsertTable(ls_sql, null);
            }
            finally
            {
                if (importDA != null)
                    importDA.CloseConnection();
            }
        }
    }
}
