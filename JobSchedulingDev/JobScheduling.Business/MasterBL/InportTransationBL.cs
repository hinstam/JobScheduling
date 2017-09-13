using EG.CCAS.DataAccess.MasterDA;
using EG.CCAS.Entity.CommModel;
using EG.CCAS.Model.CommModel;
using EG.CCAS.Model.MasterModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace EG.CCAS.Business.MasterBL
{
    public class InportTransationBL : Business
    {
        
        /// <summary>
        /// Get Bank by uid
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public virtual InportTransationM SaveFileToDB(string filenpath)
        {
            InportTransationM model = new InportTransationM();
            InportTransationDA inportDA = new InportTransationDA();
            string ls_sheet, ls_cretable,ls_sql,ls_store,ls_region="";
            ls_sheet = "oct-2014";
            long ll_err_num=0;
            OleDbConnection csv_cnn = new OleDbConnection();
            csv_cnn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filenpath + ";Extended Properties='Excel 12.0;HDR=YES'";
            DataTable ldt_csv = new DataTable();
            OleDbDataAdapter lda_csv = new OleDbDataAdapter("SELECT *  FROM [" + ls_sheet + "$]", csv_cnn);
            lda_csv.Fill(ldt_csv);
            //ls_cretable = "create table #temp (StoreCode varchar(50),TransactionDate date,DocumentNo varchar(50),CreditCardNo varchar(50),BaseAmt varchar(50),bin varchar(50) )";
            ls_sql = "insert into t_ccas_transaction_master(Region,StoreCode,TransactionDate,DocumentNo,CreditCardNo,BaseAmount,BIN,CreatedDate,LastUpdateDate,UpdatedBy) values ";
            ls_sql += "(@Region,@StoreCode,@TransactionDate,@DocumentNo,@CreditCardNo,@BaseAmount,@BIN,@CreatedDate,@LastUpdateDate,@UpdatedBy)";

            model.NumOfData = ldt_csv.Rows.Count;
            for (int i = 0; i < ldt_csv.Rows.Count; i++)
            {
                ls_store=ldt_csv.Rows[i]["store"].ToString ();
                if (ls_store.Substring(0, 1) == "1")
                    ls_region = "HK";
                else if (ls_store.Substring(0, 1) == "2")
                    ls_region = "MO";
                else if (ls_store.Substring(0, 1) == "6")
                    ls_region = "CN";
                else if (ls_store.Substring(0, 1) == "7")
                    ls_region = "SG";

                Dictionary<string, object> paramsValue = new Dictionary<string, object>() { 
                                                        {"Region",ls_region},
                                                        {"StoreCode",ldt_csv.Rows[i][0].ToString () },
                                                        {"TransactionDate",DateTime.Parse(ldt_csv.Rows[i][1].ToString() ).ToShortDateString ()},
                                                        { "DocumentNo", ldt_csv.Rows[i][2].ToString () },
                                                        { "CreditCardNo", ldt_csv.Rows[i][3].ToString () },
                                                        { "BaseAmount", ldt_csv.Rows[i][4].ToString () },
                                                        { "BIN", ldt_csv.Rows[i][5].ToString () },
                                                        { "CreatedDate", DateTime.Now.ToShortDateString () },
                                                        { "LastUpdateDate", DateTime.Now.ToShortDateString () },
                                                        {"UpdatedBy","System"}};

                try
                {

                    int rel = inportDA.InsertTable(ls_sql, paramsValue);

                    if(rel<=0)
                        ll_err_num++;
                }
                catch
                {
                    ll_err_num++;
                }
            }


            model.NumOfFail = ll_err_num;
            model.NumOfSuccess = model.NumOfData - ll_err_num;
            model.HasResult = true;
            inportDA.CloseConnection();
                //int i = InportTransationDA.CreateTempTable(ls_cretable);


                // if (i != 0)



                //   return BankDA.RowToEntity(dr);
                //  else
            return model;
        }
    }
}
