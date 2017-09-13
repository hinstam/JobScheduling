using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobScheduling.Model.MasterModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using JobScheduling.DBCommon;
using JobScheduling.Common;
using System.Configuration;

namespace JobScheduling.DataAccess.MasterDA
{
    public class ImportStoreFileDA:Repository
    {
        private const string STORE_HK_IMPORT_SELECT = @"select Site as storecode,[Search Term 1] as searchterm from ['hk sap search term$'] where [Search Term 1] not in ('REFERENCE DC','REFERENCE SITE')";
        private const string STORE_PRC_IMPORT_SELECT = @"select [店鋪代碼_(4 digs)] as storecode,搜索項 as searchterm from [Site$] where [店鋪代碼_(4 digs)] is not null";

        private const string STORE_TEMP_CREATE = @"create table #new_store(
StoreCode nvarchar(6) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS primary key,
District nvarchar(6) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null,
StoreDescription nvarchar(15) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null
)";
        private const string STORE_IMPORT = @"insert into t_ccas_store_master(storecode,district,StoreDescription,CreatedDate,LastUpdateDate,UpdatedBy) select a.*,getdate(),getdate(),@UpdatedBy UpdatedBy from #new_store a where a.storecode not in (select storecode from t_ccas_store_master)";

        private const string STORE_REPEAT_LIST = @"select * from #new_store where storecode in (select storecode from t_ccas_store_master)";


        public ImportStoreFileM SaveFileToDB(string filePath, string ls_exportpath, string webPath,string userID)
        {
            ImportStoreFileM importM = new ImportStoreFileM();
            string oleCon = string.Empty;
            string sheetName=string.Empty;
            string oleSql = string.Empty;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                string ss = FileHelper.GetFileExtension(filePath);
                if (FileHelper.GetFileExtension(filePath)=="xlsx")
                {
                    oleCon = string.Format(ConfigurationManager.ConnectionStrings["XlsxOledbCon"].ToString(), filePath); 
                }
                if (FileHelper.GetFileExtension(filePath)=="xls")
                {
                    oleCon = string.Format(ConfigurationManager.ConnectionStrings["XlsOledbCon"].ToString(), filePath); 
                }

                if (filePath.ToLower().Contains("hk"))
                {
                    sheetName = ConfigurationManager.AppSettings["hk"].ToString();
                    oleSql = STORE_HK_IMPORT_SELECT;
                }
                if (filePath.ToLower().Contains("prc"))
                {
                    sheetName = ConfigurationManager.AppSettings["prc"].ToString();
                    oleSql = STORE_PRC_IMPORT_SELECT;
                }

                ds = OleDBHelper.ExecuteDataset(oleCon, CommandType.Text, oleSql);
                dt = ds.Tables[0];

                if (dt == null & dt.Rows.Count != 0)
                {
                    return importM;
                }

                dt.Columns.Add("StoreDescription", typeof(string));
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    if (dt.Rows[i][0].ToString() == "6892")
                        importM.Errmsg = string.Empty;
                   string[] searchItemList=dt.Rows[i][1].ToString().Split('-');
                    if(searchItemList.Length==3)
                    {
                        dt.Rows[i]["StoreDescription"] = searchItemList[2];
                        dt.Rows[i][1]=searchItemList[1];
                        continue;
                    }
                    if(searchItemList.Length==2)
                    {
                        dt.Rows[i]["StoreDescription"] = searchItemList[1];
                        dt.Rows[i][1]=null;
                    }
                }

                string sqlConnectionStr=ConfigurationManager.ConnectionStrings["EGCCASEntities"].ConnectionString;
                using (SqlConnection sqlCon = new SqlConnection(sqlConnectionStr))
                {
                    sqlCon.Open();
                    SqlTransaction trans = sqlCon.BeginTransaction();

                    try
                    {
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, STORE_TEMP_CREATE);

                        using (SqlBulkCopy sqlBulk = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.Default, trans))
                        {
                            sqlBulk.DestinationTableName = @"#new_store";
                            sqlBulk.BatchSize = dt.Rows.Count;
                            sqlBulk.ColumnMappings.Add("storecode", "StoreCode");
                            sqlBulk.ColumnMappings.Add("searchterm", "District");
                            sqlBulk.ColumnMappings.Add("StoreDescription", "StoreDescription");
                            sqlBulk.BulkCopyTimeout = 3600;
                            sqlBulk.WriteToServer(dt);
                        }

                        SqlParameter[] sqlParamList = new SqlParameter[] {
                        new SqlParameter("@UpdatedBy",userID)
                        };
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text,STORE_IMPORT,sqlParamList);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }
 
                }





            }
            catch (Exception ex)
            {
                importM.Errmsg = ex.Message;
            }
            finally
            {

            }

            return importM;
        }


        public int InsertTable(string sql, Dictionary<string, object> paramValues)
        {
            return Template.Execute(sql, paramValues);
        }
    }
}
