using JobScheduling.Entity.CommModel;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.MasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace JobScheduling.DataAccess.MasterDA
{
    public class ImportBinFileDA : Repository
    {
        public int InsertTable(string sql, Dictionary<string, object> paramValues)
        {
            return Template.Execute(sql, paramValues);
        }

        public int UpdateTransactionMaster(string sql)
        {
            return Template.Execute(sql, null);
        }


        public List<string>  ComparTable(DataTable dt, ref long ll_skip_num, ref DataTable ldt_same)
        {
            SqlConnection sqlcn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EGCCASEntities"].ConnectionString);
            SqlBulkCopy bcp = new SqlBulkCopy(sqlcn);
            SqlCommand pos_cmd = new SqlCommand();
            string ls_sql;

            string ls_cretable = @"create table #temp (BIN nvarchar(100) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS ,
                CardBrand nvarchar(100) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null,
                IssuingBank nvarchar(100) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null,
                TypeofCard nvarchar(100)  COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null,
                CategoryofCard nvarchar(100)  COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null,
                IssuingCountryISOName nvarchar(100) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null,
                Otherinfo nvarchar(110) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null,
                IssuingCountryISOA2Code nvarchar(100) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null,
                IssuingCountryISOA3Code nvarchar(100) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null,
                IssuingCountryISOnumber nvarchar(100) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null,
                Website nvarchar(100) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null,
                Phone nvarchar(100) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null,
                FormerBank nvarchar(100) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null,
                Address nvarchar(100) COLLATE Chinese_Hong_Kong_Stroke_90_CI_AS null)";
            sqlcn.Open();
            pos_cmd.Connection = sqlcn;
            pos_cmd.CommandText = ls_cretable;
            pos_cmd.ExecuteNonQuery();
            // bcp.SqlRowsCopied += new System.Data.SqlClient.SqlRowsCopiedEventHandler();
            bcp.BatchSize = 1000;//每次传输的行数
            bcp.NotifyAfter = 100;//进度提示的行数
            bcp.DestinationTableName = "#temp";//目标表
            bcp.WriteToServer(dt);

            ls_sql = "select count(*) as cnt from t_ccas_bin_master a,#temp b where a.bin=b.bin and a.ManualAuto='M'";
            pos_cmd.CommandText = ls_sql;
            pos_cmd.ExecuteNonQuery();

            SqlDataReader sdr = pos_cmd.ExecuteReader();
            sdr.Read();
            ll_skip_num = long.Parse(sdr["cnt"].ToString());
            sdr.Close();
            ls_sql = "select b.* from t_ccas_bin_master a,#temp b where a.bin=b.bin";
            SqlDataAdapter sda = new SqlDataAdapter(ls_sql, sqlcn);
            //DataTable ldt_same = new DataTable();
            sda.Fill(ldt_same);

            List<string> list = new List<string>();
            if (ldt_same.Rows.Count > 0)
            {
                for (int i = 0; i < ldt_same.Rows.Count; i++)
                {
                    //foundRow = dt.Select("bin ='" + ldt_same.Rows[i]["bin"].ToString().Trim() + "'");
                    //foreach (DataRow row in foundRow)
                    //{
                    //    dt.Rows.Remove(row);
                    //}
                    list.Add(ldt_same.Rows[i]["bin"].ToString());
                }

                ls_sql = @"update t_ccas_bin_master set CardBrand=left(b.CardBrand,20),IssuingBank=left(b.IssuingBank,30),
                    TypeofCard=left(b.TypeofCard,20),CategoryofCard=left(b.CategoryofCard,20),IssuingCountryISOA2Code=left(b.IssuingCountryISOA2Code,2), 
                    LastUpdateDate=getdate(),UpdatedBy='System' from t_ccas_bin_master a inner join #temp b on a.bin=b.bin and a.ManualAuto='A'";
                pos_cmd.CommandText = ls_sql;
                int re=pos_cmd.ExecuteNonQuery();
            }
            sda.Dispose();
            sqlcn.Close();
            return list;
            
        }



    }
}
