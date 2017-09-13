using JobScheduling.Entity.CommModel;
using JobScheduling.Model.FileModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JobScheduling.DataAccess.FileDA
{
    public class TransactionFileDA : Repository
    {
        private const string TEXT_GetTransactionFileByCode = "select * from t_ccas_transaction_master where  StoreCode=@StoreCode AND SerialNo=@SerialNo AND DocumentNo=@DocumentNo";

//        private const string TEXT_InsertTransactionFileByCode = @"insert into t_ccas_transaction_master(ManualAuto,BIN,CardBrand,IssuingBank,
//            TypeofCard,CategoryofCard,IssuingCountryISOA2Code,CreatedDate,LastUpdateDate,UpdatedBy ) values 
//            (@ManualAuto,@BIN,@CardBrand,@IssuingBank,@TypeofCard,@CategoryofCard,@IssuingCountryISOA2Code,@CreatedDate,@LastUpdateDate,@UpdatedBy)";

         //string TEXT_UpdateTransactionFileByCode = "update t_ccas_transaction_master set Region=@Region,TransactionDate=@TransactionDate,";
         //       TEXT_UpdateTransactionFileByCode+ =" CreditCardNo=@CreditCardNo,BaseAmount=@BaseAmount,BIN=@BIN,CardBrand=@CardBrand,";
         //       TEXT_UpdateTransactionFileByCode+ ="IssuingBank=@IssuingBank,TypeofCard=@TypeofCard,CategoryofCard=@CategoryofCard,IssuingCountryCode=@IssuingCountryCode,";
         //       TEXT_UpdateTransactionFileByCode+ ="LastUpdateDate=@LastUpdateDate,UpdatedBy= @UpdatedBy where SerialNo=@SerialNo AND StoreCode=@StoreCode AND DocumentNo=@DocumentNo";

        private const string TEXT_DelTransactionFileByCode = "delete from t_ccas_transaction_master where StoreCode=@StoreCode AND SerialNo=@SerialNo AND DocumentNo=@DocumentNo";

        public DataRow GetTransactionFileByCode(string code, string SerialNo, string DocumentNo)
        {
            DataTable dt = Template.Query(TEXT_GetTransactionFileByCode, new string[] { "@StoreCode", "SerialNo", "DocumentNo" }, new object[] { code, SerialNo, DocumentNo });
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int NewTransactionFile(string sql, Dictionary<string, object> paramValues)
        {
            return Template.Execute(sql, paramValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramsValue"></param>
        /// <returns></returns>
        public int UpdateTransactionFile(string sql, Dictionary<string, object> paramValues)
        {
            return Template.Execute(sql, paramValues);

            //return Template.Execute(TEXT_UpdateTransactionFileByCode, new string[] { "@Region,@SerialNo", "@StoreCode", "@CardBrand", "@IssuingBank", "@TypeofCard",
            //    "@CategoryofCard","@IssuingCountryISOA2Code","@LastUpdateDate","@UpdatedBy" }, paramsValue);
        }

        public int Delete(Dictionary<string, object> paramValues)
        {
            return Template.Execute(TEXT_DelTransactionFileByCode, paramValues);
        }

        public PagingModel GetTransactionFileByPage(string StoreCode, string SerialNo, string DocumentNo, PagingModel pm)
        {
            int totalCount = 0;

            StringBuilder SelectSQL = new StringBuilder("SELECT  StoreCode,SerialNo,Region,CONVERT(varchar(12) , TransactionDate, 111 ) as 'Transaction Date',DocumentNo,CreditCardNo,BaseAmount,BIN,IssuingCountryCode as 'Issuing Country Code' ");
            SelectSQL.Append(" from t_ccas_transaction_master where 1=1 ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(StoreCode))
            {
                SelectSQL.Append(" and StoreCode like @StoreCode  ");
                pvs.Add("@StoreCode", "%" + StoreCode + "%");
            }

            if (!string.IsNullOrEmpty(SerialNo))
            {
                SelectSQL.Append(" and SerialNo like @SerialNo  ");
                pvs.Add("@SerialNo", "%" + SerialNo + "%");
            }

            if (!string.IsNullOrEmpty(DocumentNo))
            {
                SelectSQL.Append(" and DocumentNo like @DocumentNo ");
                pvs.Add("@DocumentNo", "%" + DocumentNo + "%");
            }

            SelectSQL.Append(" order by SerialNo");

            DataTable dt = Template.QueryByPage(SelectSQL.ToString(), pvs, pm.PageSize, pm.PageIndex, out totalCount);

            pm.DataTable = dt;
            pm.TotalCount = totalCount;

            return pm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public TransactionFileM Row2Object(DataRow row)
        {
            TransactionFileM model = null;
            if (row != null)
            {
                model = new TransactionFileM();
                model.Region = row["Region"] == DBNull.Value ? null : (string)row["Region"];
                model.TransactionDate = row["TransactionDate"] == DBNull.Value ? null : DateTime.Parse(row["TransactionDate"].ToString()).ToString("yyyyMMdd");
                model.CreditCardNo = row["CreditCardNo"] == DBNull.Value ? null : (string)row["CreditCardNo"];
                model.BaseAmount = row["BaseAmount"] .ToString ();
                model.BIN = row["BIN"] == DBNull.Value ? null : (string)row["BIN"];
                model.CardBrand = row["CardBrand"] == DBNull.Value ? null : (string)row["CardBrand"];
                model.IssuingBank = row["IssuingBank"] == DBNull.Value ? null : (string)row["IssuingBank"];
                model.TypeofCard = row["TypeofCard"] == DBNull.Value ? null : (string)row["TypeofCard"];
                model.CategoryofCard = row["CategoryofCard"] == DBNull.Value ? null : (string)row["CategoryofCard"];
                model.IssuingCountryCode = row["IssuingCountryCode"] == DBNull.Value ? null : (string)row["IssuingCountryCode"];
                model.CreatedDate = DateTime.Parse ( row["CreatedDate"].ToString ());
                model.LastUpdateDate = DateTime.Now;
                model.UpdatedBy = row["UpdatedBy"] == DBNull.Value ? null : (string)row["UpdatedBy"];
                model.SerialNo = row["SerialNo"] == DBNull.Value ? null : (string)row["SerialNo"];
                model.StoreCode = row["StoreCode"] == DBNull.Value ? null : (string)row["StoreCode"];
                model.DocumentNo = row["DocumentNo"] == DBNull.Value ? null : (string)row["DocumentNo"];


                //model.BIN = (string)row["BIN"];
                ////    model.ManualAuto = row["ManualAuto"] == DBNull.Value ? null : (string)row["ManualAuto"];
                //model.CardBrand = row["CardBrand"] == DBNull.Value ? null : (string)row["CardBrand"]; ;
                //model.CategoryofCard = row["CategoryofCard"] == DBNull.Value ? null : (string)row["CategoryofCard"]; ;
                //model.IssuingBank = row["IssuingBank"] == DBNull.Value ? null : (string)row["IssuingBank"];
                ////     model.IssuingCountryISOA2Code = row["IssuingCountryISOA2Code"] == DBNull.Value ? null : (string)row["IssuingCountryISOA2Code"];
                //model.TypeofCard = row["TypeofCard"] == DBNull.Value ? null : (string)row["TypeofCard"];

            }
            return model;
        }
    }
}
