using JobScheduling.DataAccess.MasterDA;
using JobScheduling.Entity.CommModel;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.MasterModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace JobScheduling.Business.MasterBL
{
    public class ImportBinFileBL : Business
    {
        public virtual ImportBinFileM SaveFileToDB(string filenpath, string ls_exportpath, string webPath)
        {
            ImportBinFileM model = new ImportBinFileM();
            ImportBinFileDA importDA = null;
            string ls_cretable, ls_sql, ls_store, ls_region = "";
            long ll_err_num = 0, ll_skip_num=0;
            OleDbConnection csv_cnn = new OleDbConnection();
            try
            {
                importDA = new ImportBinFileDA();
                if (filenpath.Substring(filenpath.Length - 4, 4) == "xlsx")
                {
                    csv_cnn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filenpath + ";Extended Properties='Excel 12.0;HDR=YES'";
                }
                if (filenpath.Substring(filenpath.Length - 3, 3) == "xls")
                {
                    csv_cnn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filenpath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";
                }

                // csv_cnn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filenpath + ";Extended Properties='Excel 8.0;HDR=YES';";
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
                //if (!ldt_csv.Columns.Contains("BIN") || !ldt_csv.Columns.Contains("Card Brand") || !ldt_csv.Columns.Contains("Bank") || !ldt_csv.Columns.Contains("Card Type") || !ldt_csv.Columns.Contains("Card Cat") || !ldt_csv.Columns.Contains("Country") || !ldt_csv.Columns.Contains("Option") || !ldt_csv.Columns.Contains("A2") || !ldt_csv.Columns.Contains("A3") || !ldt_csv.Columns.Contains("ISO No") || !ldt_csv.Columns.Contains("Website") || !ldt_csv.Columns.Contains("Phone") || !ldt_csv.Columns.Contains("Former") || !ldt_csv.Columns.Contains("Address"))
                //{    
                    
                //    model.Errmsg = "This is not a Bin File,Please check it";
                //    csv_cnn.Close();
                //    importDA.CloseConnection();
                //    return model;
                //}
                model.NumOfData = ldt_csv.Rows.Count;

                /**********export*************/
                //Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application();
                //Microsoft.Office.Interop.Excel.Workbooks myBooks = myExcel.Workbooks;
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

                    string exportFile = filepath + "\\ExportBin.xlsx";


                    csv_cnn2.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + exportFile + ";Extended Properties='Excel 12.0 Xml;HDR=YES'";

                    csv_cnn2.Open();
                    OleDbCommand cmd = new OleDbCommand(@"create table [sheet1]([BIN] Text,[CardBrand] Text,[IssuingBank] Text,[TypeofCard] Text,
                        [CategoryofCard] Text,[IssuingCountryISOName] Text,[Otherinfo] Text,[IssuingCountryISOA2Code] Text,[IssuingCountryISOA3Code] Text,
                        [IssuingCountryISOnumber] Text,[Website] Text,[Phone] Text,[FormerBank] Text,[Address] Text)", csv_cnn2);
                    cmd.ExecuteNonQuery();


                }
                catch (Exception er)
                {
                    model.Errmsg = "Error excel file create fail ";
                    csv_cnn2.Close();
                    importDA.CloseConnection();
                    return model;
                }

                DataTable ldt_export = new DataTable();
                //DataTable ldt_ref = ImportDA.ComparTable(ldt_csv,ref ll_skip_num, ref ldt_export);
                DataTable ldt_ref = ldt_csv;
                List<string> list = importDA.ComparTable(ldt_csv, ref ll_skip_num, ref ldt_export);


                //if (ldt_export.Rows.Count > 0)
                //{
                //    for (int r = 0; r < ldt_export.Rows.Count; r++)
                //    {
                //        //for (int i = 0; i < ldt_export.Columns.Count; i++)
                //        //{
                //        //    worksheet.Cells[r + 2, i + 1] = ldt_export.Rows[r][i].ToString();

                //        //}
                //        WriteErrorToExcel(csv_cnn2, ldt_export.Rows[r]);
                //    }
                //}

                //ll_err_num = ldt_export.Rows.Count;
                ll_err_num = 0;

                ls_sql = @"insert into t_ccas_bin_master(ManualAuto,BIN,CardBrand,IssuingBank,TypeofCard,CategoryofCard,IssuingCountryISOA2Code,CreatedDate,LastUpdateDate,UpdatedBy) values
                           ('A',@BIN,@CardBrand,@IssuingBank,@TypeofCard,@CategoryofCard,@IssuingCountryISOA2Code,@CreatedDate,@LastUpdateDate,@UpdatedBy)";

                string CardBrand = "", IssuingBank = "", TypeofCard = "", CategoryofCard;
                for (int i = 0; i < ldt_ref.Rows.Count; i++)
                {
                    if (ldt_ref.Rows[i]["A2"].ToString().Trim().Length == 2 || ldt_ref.Rows[i]["A2"].ToString().Trim().Length == 0)
                    {
                        if (list.Contains(ldt_ref.Rows[i]["BIN"].ToString()))
                            continue;

                        if (ldt_csv.Rows[i]["Card Brand"].ToString().Length > 20)
                        {
                            CardBrand = ldt_csv.Rows[i]["Card Brand"].ToString().Substring(0, 20);
                        }
                        else
                            CardBrand = ldt_csv.Rows[i]["Card Brand"].ToString();

                        if (ldt_csv.Rows[i]["Bank"].ToString().Length > 30)
                        {
                            IssuingBank = ldt_csv.Rows[i]["Bank"].ToString().Substring(0, 30);
                        }
                        else
                            IssuingBank = string.IsNullOrEmpty(ldt_csv.Rows[i]["Bank"].ToString()) ? "Unclassified" : ldt_csv.Rows[i]["Bank"].ToString();

                        if (ldt_csv.Rows[i][3].ToString().Length > 20)
                        {
                            TypeofCard = ldt_csv.Rows[i]["Card Type"].ToString().Substring(0, 20);
                        }
                        else
                            TypeofCard = ldt_csv.Rows[i]["Card Type"].ToString();

                        if (ldt_csv.Rows[i]["Card Cat"].ToString().Length > 20)
                        {
                            CategoryofCard = ldt_csv.Rows[i]["Card Cat"].ToString().Substring(0, 20);
                        }
                        else
                            CategoryofCard = ldt_csv.Rows[i]["Card Cat"].ToString();

                        Dictionary<string, object> paramsValue = new Dictionary<string, object>() { 
                                                        {"BIN",ldt_ref.Rows[i]["BIN"].ToString ()},
                                                        {"CardBrand",CardBrand },
                                                        {"IssuingBank",IssuingBank },
                                                        { "TypeofCard", TypeofCard },
                                                        { "CategoryofCard", CategoryofCard },
                                                        { "IssuingCountryISOA2Code", ldt_ref.Rows[i]["A2"].ToString () },
                                                        { "CreatedDate", DateTime.Now},
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
                                //    worksheet.Cells[ll_err_num + 1, k + 1] = ldt_ref.Rows[i][k].ToString();
                                //}
                                WriteErrorToExcel(csv_cnn2, ldt_ref.Rows[i]);
                            }
                        }
                        catch (Exception e)
                        {
                            ll_err_num++;
                            //for (int k = 0; k < ldt_csv.Columns.Count; k++)
                            //{
                            //    worksheet.Cells[ll_err_num + 1, k + 1] = ldt_ref.Rows[i][k].ToString();
                            //}
                            WriteErrorToExcel(csv_cnn2, ldt_ref.Rows[i]);
                        }
                    }
                    else
                    {
                        ll_err_num++;
                        //for (int k = 0; k < ldt_csv.Columns.Count; k++)
                        //{
                        //    worksheet.Cells[ll_err_num + 1, k + 1] = ldt_ref.Rows[i][k].ToString();
                        //}
                        WriteErrorToExcel(csv_cnn2, ldt_ref.Rows[i]);
                    }

                }


                ls_sql = "update t_ccas_transaction_master set CardBrand=b.CardBrand,IssuingBank=b.IssuingBank,TypeofCard=b.TypeofCard,CategoryofCard=b.CategoryofCard,IssuingCountryCode=b.IssuingCountryISOA2Code from t_ccas_transaction_master a inner join t_ccas_bin_master b on a.bin=b.bin";
                importDA.UpdateTransactionMaster(ls_sql);

                //string exportpath = DateTime.Now.ToFileTime().ToString();
                //string filepath = ls_exportpath + "\\" + exportpath;
                //if (ll_err_num > 0)
                //{
                //    if (!Directory.Exists(filepath))//判断文件夹是否已经存在
                //    {
                //        Directory.CreateDirectory(filepath);//创建文件夹
                //    }
                //    workbook.SaveAs(filepath + "\\ExportBin.xlsx", oMissing, oMissing, oMissing, oMissing, oMissing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, oMissing, oMissing, oMissing, oMissing, oMissing);
                //}
                ////关闭文件  
                //workbook.Close(false, filepath + "\\ExportBin.xlsx", true);
                //myExcel.Quit();
                GC.Collect();
                model.ExportFilename = "http://" + webPath + "/upload/" + exportpath + "/ExportBin.xlsx";
                string insertsql = "insert into t_ccas_export_master values (@ExportDate,@Path,@Kind,@CreatedBy)";
                Dictionary<string, object> pv = new Dictionary<string, object>() { 
                                    {"ExportDate",DateTime.Now},
                                    {"Path",model.ExportFilename },
                                    { "Kind", "Bin File" },
                                    {"CreatedBy",UserID}};

                int re = importDA.InsertTable(insertsql, pv);
                model.NumOfSkip = ll_skip_num;
                model.NumOfFail = ll_err_num;
                model.NumOfSuccess = model.NumOfData - ll_err_num;
                model.HasResult = true;
                csv_cnn2.Close();
            }
            catch (Exception e)
            {
                model.Errmsg = e.Message;                            
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
            string strSQL = @"INSERT INTO [Sheet1] ([BIN] ,[CardBrand] ,[IssuingBank] ,[TypeofCard] ,
                        [CategoryofCard] ,[IssuingCountryISOName] ,[Otherinfo] ,[IssuingCountryISOA2Code] ,[IssuingCountryISOA3Code] ,
                        [IssuingCountryISOnumber] ,[Website] ,[Phone] ,[FormerBank] ,[Address] ) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

            OleDbCommand cmd = new OleDbCommand(strSQL, csv_cnn2);

            for (int i = 0; i < 14; i++)
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
                string ls_sql = "truncate table t_ccas_bin_master";
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
