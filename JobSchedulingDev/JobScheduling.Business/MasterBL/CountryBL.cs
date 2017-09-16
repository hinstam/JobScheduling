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
using System.Web;
 

namespace JobScheduling.Business.MasterBL
{
    public class CountryBL : Business
    {
        public ResultModel New(CountryM model)
        {
            ResultModel result = new ResultModel();
            DateTime dt = DateTime.Now;

            CountryDA countryDA = null;
            try
            {
                countryDA = new CountryDA();

                if (countryDA.GetCountry(model.Code) != null)
                {
                    result.IsSuccess = false;
                    result.Exception = "Country has been saved!";
                    return result;
                }

                Dictionary<string, object> paramValues = new Dictionary<string, object>();
                paramValues.Add("Code", model.Code.Trim());
                paramValues.Add("Description", model.Description);
                paramValues.Add("CreatedDate", dt);
                paramValues.Add("UpdatedBy", UserID);
                paramValues.Add("LastUpdateDate", dt);


                result.Affected = countryDA.NewCountry(paramValues);
            }
            catch(Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            finally
            {
                if (countryDA != null)
                    countryDA.CloseConnection();
            }
            return result;
        }


        public PagingModel List(string code, string description, int pageSize, int pageIndex)
        {
            CountryDA countryDA = null;
            PagingModel dt = new PagingModel();
            try
            {
                countryDA = new CountryDA();

                dt = countryDA.GetCountryByPage(code, description, new PagingModel() { PageIndex = pageIndex, PageSize = pageSize });
            }
            finally
            {
                if (countryDA != null)
                    countryDA.CloseConnection();
            }

            return dt;
        }


        public CountryM GetCountry(string code)
        {
            DataTable dt = new DataTable();
            CountryM u = new CountryM();
            CountryDA countryDA = null;
            
            try
            {
                countryDA = new CountryDA();

                dt = countryDA.GetCountry(code);



                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    u.Code = dr["Code"] == null ? "" : dr["Code"].ToString();
                    u.Description = dr["Description"] == null ? "" : dr["Description"].ToString();
                }
            }
            finally
            {
                if (countryDA != null)
                    countryDA.CloseConnection();
            }
            return u;
        }


        public ResultModel Edit(CountryM model)
        {
            ResultModel result = new ResultModel();

            CountryDA countryDA = null;

            try
            {
                countryDA = new CountryDA();

                Dictionary<string, object> saveData = new Dictionary<string, object>();

                saveData.Add("Code", model.Code.Trim());
                saveData.Add("Description", model.Description);
                saveData.Add("UpdatedBy", UserID);
                saveData.Add("LastUpdateDate", DateTime.Now);

                result.Affected = countryDA.EditCountry(saveData, model.Code);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            finally
            {
                if (countryDA != null)
                    countryDA.CloseConnection();
            }

            return result;
        }


        public ResultModel Delete(string code)
        {
            ResultModel result = new ResultModel();

            CountryDA countryDA = null;

            try
            {
                countryDA = new CountryDA();

                result.Affected = countryDA.DelCountry(code);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            finally
            {
                if (countryDA != null)
                    countryDA.CloseConnection();
            }

            return result;
        }

        public virtual CountryM SaveFileToDB(string filenpath, string ls_exportpath,string webPath)
        {
            CountryM model = new CountryM();
            CountryDA countryDA = null;
            string ls_sql, ls_filepath, ls_description;
            long ll_err_num = 0;
            OleDbConnection csv_cnn = new OleDbConnection();
            try
            {
                countryDA = new CountryDA();
                if (filenpath.Substring(filenpath.Length - 4, 4) == "xlsx")
                {
                    csv_cnn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filenpath + ";Extended Properties='Excel 12.0 Xml;HDR=YES'";
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

                // string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                DataTable ldt_csv = new DataTable();
                OleDbDataAdapter lda_csv = new OleDbDataAdapter("SELECT *  FROM [" + SheetName + "] where [ISO A2 Code] is not null and [Country Description] is not null", csv_cnn);
                try
                {
                    lda_csv.Fill(ldt_csv);
                }
                catch (Exception e)
                {
                    model.Errmsg = "Can't find the sheet name in the current Excel File";
                    csv_cnn.Close();
                    countryDA.CloseConnection();
                    return model;
                }
                if (!ldt_csv.Columns.Contains("Country Description") || !ldt_csv.Columns.Contains("ISO A2 Code"))
                {
                    model.Errmsg = "This is not a Country File,Please check it";
                    csv_cnn.Close();
                    countryDA.CloseConnection();
                    return model;
                }
                if (ldt_csv.Rows.Count == 0)
                {
                    model.Errmsg = "Country Description and ISO A2 Code can not be null";
                    csv_cnn.Close();
                    countryDA.CloseConnection();
                    return model;
                }
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

                    string exportFile = filepath + "\\ExportCountry.xlsx";


                    csv_cnn2.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + exportFile + ";Extended Properties='Excel 12.0 Xml;HDR=YES'";

                    csv_cnn2.Open();
                    OleDbCommand cmd = new OleDbCommand("create table [sheet1]([Country Description] Text,[ISO A2 Code] Text)", csv_cnn2);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                }
                catch (Exception er)
                {
                    model.Errmsg = "Error excel file create fail ";
                    csv_cnn2.Close();
                    countryDA.CloseConnection();
                    return model;
                }

                ls_sql = @"insert into t_md_country(Code,Description,CreatedDate,LastUpdateDate,UpdatedBy) values
                           (@Code,@Description,@CreatedDate,@LastUpdateDate,@UpdatedBy)";

                model.NumOfData = ldt_csv.Rows.Count;
                for (int i = 0; i < ldt_csv.Rows.Count; i++)
                {
                    if (ldt_csv.Rows[i]["Country Description"].ToString().Length > 32)
                    {
                        ls_description = ldt_csv.Rows[i]["Country Description"].ToString().Substring(0, 32);
                    }
                    else
                        ls_description = ldt_csv.Rows[i]["Country Description"].ToString();

                    Dictionary<string, object> paramsValue = new Dictionary<string, object>() { 
                                                    {"Code",ldt_csv.Rows[i]["ISO A2 Code"].ToString ()},
                                                    {"Description",ls_description },
                                                    { "CreatedDate", DateTime.Now },
                                                    { "LastUpdateDate", DateTime.Now },
                                                    {"UpdatedBy","System"}};
                    try
                    {

                        int rel = countryDA.InsertTable(ls_sql, paramsValue);

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
                //    //workbook.SaveAs(filepath + "\\ExportCountry.xlsx", oMissing, oMissing, oMissing, oMissing, oMissing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, oMissing, oMissing, oMissing, oMissing, oMissing);
                //}
                //关闭文件  
                //workbook.Close(false, filepath + "\\ExportCountry.xlsx", true);
                //myExcel.Quit();
                GC.Collect();

                model.ExportFilename = "http://" + webPath + "/upload/" + exportpath + "/ExportCountry.xlsx";

                string insertsql = "insert into t_ccas_export_master values (@ExportDate,@Path,@Kind,@CreatedBy)";
                Dictionary<string, object> pv = new Dictionary<string, object>() { 
                                    {"ExportDate",DateTime.Now},
                                    {"Path",model.ExportFilename },
                                    { "Kind", "Country File" },
                                    {"CreatedBy",UserID}};

                int re = countryDA.InsertTable(insertsql, pv);

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
                if (countryDA != null)
                    countryDA.CloseConnection();
            }
            return model;
        }


        private void WriteErrorToExcel(OleDbConnection csv_cnn2, DataRow row)
        {
            string strSQL = "INSERT INTO [Sheet1] ([Country Description], [ISO A2 Code]) VALUES (?, ?)";

            OleDbCommand cmd = new OleDbCommand(strSQL, csv_cnn2);

            for (int i = 0; i < 2; i++)
            {
                cmd.Parameters.Add(i.ToString(), OleDbType.VarChar);
            }

            cmd.Parameters[0].Value = row[0];
            cmd.Parameters[1].Value = row[1];

            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public void DeleteData()
        {
            ImportTransactionDA importDA = null;
            try
            {
                importDA = new ImportTransactionDA();
                string ls_sql = "truncate table t_md_country";
                int re = importDA.InsertTable(ls_sql, null);
            }
            finally
            {
                if (importDA != null)
                    importDA.CloseConnection();
            }
        }

        public IList<CountryM> GetAllCountry(string code)
        {
            IList<CountryM> countryList = new List<CountryM>();
            DataTable dt = new DataTable();
            CountryDA countryDA = null;
            try
            {
                countryDA = new CountryDA();
                dt = countryDA.GetAllCountry(code);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CountryM countryM = new CountryM();
                        countryM.Description = dr["Description"] != null ? dr["Description"].ToString() : string.Empty ;
                        countryM.Code = dr["code"] != null ? dr["code"].ToString() : string.Empty;
                        countryList.Add(countryM);
                    }
                }
            }
            finally
            {
                if (countryDA != null)
                    countryDA.CloseConnection();
            }
            return countryList;
        }
    }
}
