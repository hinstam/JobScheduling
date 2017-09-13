using JobScheduling.DataAccess.FileDA;
using JobScheduling.Entity.CommModel;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.FileModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JobScheduling.Business.FileBL
{
    public class TransactionFileBL : Business
    {
        public ResultModel NewTransactionFile(TransactionFileM model)
        {
            ResultModel resultModel = new ResultModel();
            TransactionFileDA transactionFileDA = null;
            try
            {
                transactionFileDA = new TransactionFileDA();

                if (transactionFileDA.GetTransactionFileByCode(model.StoreCode, model.SerialNo, model.DocumentNo) != null)
                {
                    resultModel.IsSuccess = false;
                    resultModel.Exception = "The same record has been saved!";
                    return resultModel;
                }

                string ls_sql, ls_region = "";
                ls_sql = "insert into t_ccas_transaction_master(Region,SerialNo,StoreCode,TransactionDate,DocumentNo,CreditCardNo,BaseAmount,BIN,CreatedDate,LastUpdateDate,UpdatedBy) values ";
                ls_sql += "(@Region,@SerialNo,@StoreCode,@TransactionDate,@DocumentNo,@CreditCardNo,@BaseAmount,@BIN,@CreatedDate,@LastUpdateDate,@UpdatedBy)";

                if (model.StoreCode.Substring(0, 1) == "1")
                    ls_region = "HK";
                else if (model.StoreCode.Substring(0, 1) == "2")
                    ls_region = "MO";
                else if (model.StoreCode.Substring(0, 1) == "6")
                    ls_region = "CN";
                else if (model.StoreCode.Substring(0, 1) == "7")
                    ls_region = "SG";
                else
                    ls_region = "";

                string TransactionDate = FormatDate(model.TransactionDate);

                Dictionary<string, object> paramsValue = new Dictionary<string, object>() { 
                                                        {"Region",ls_region},
                                                        {"SerialNo",model.SerialNo},
                                                        {"StoreCode",model.StoreCode },
                                                        {"TransactionDate", DateTime.Parse(TransactionDate)},
                                                        { "DocumentNo", model.DocumentNo},
                                                        { "CreditCardNo", model.CreditCardNo },
                                                        { "BaseAmount", model.BaseAmount },
                                                        { "BIN", model.BIN},
                                                        {"CardBrand", model.CardBrand},
                                                        { "IssuingBank", model.IssuingBank},
                                                        { "TypeofCard", model.TypeofCard },
                                                        { "CategoryofCard", model.CategoryofCard },
                                                        { "IssuingCountryCode", model.IssuingCountryCode},
                                                        { "CreatedDate", DateTime.Now },
                                                        { "LastUpdateDate", DateTime.Now },
                                                        {"UpdatedBy",UserID }};


                int rel = transactionFileDA.NewTransactionFile(ls_sql, paramsValue);
                if (rel == 0)
                {
                    resultModel.IsSuccess = false;
                    resultModel.Exception = "failed";
                }
            }
            catch (Exception ex)
            {
                resultModel.IsSuccess = false;
                resultModel.Exception = ex.Message;
            }
            finally
            {
                if (transactionFileDA != null)
                    transactionFileDA.CloseConnection();
            }
            return resultModel;
        }

        /// <summary>
        /// Get TransactionFile by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public TransactionFileM GetTransactionFileByCode(string code, string SerialNo, string DocumentNo)
        {
            TransactionFileDA transactionFileDA = null; 
            TransactionFileM model = null;
            DataRow dr ;
            try
            {
                transactionFileDA = new TransactionFileDA();
                dr = transactionFileDA.GetTransactionFileByCode(code, SerialNo, DocumentNo);
                

                if (dr != null)
                    model = transactionFileDA.Row2Object(dr);
            }
            finally
            {
                if (transactionFileDA != null)
                    transactionFileDA.CloseConnection();
            }
            return model;

        }

        public ResultModel UpdateTransactionFile(TransactionFileM model)
        {
            string ls_sql, ls_region="";
            ls_sql= @"update t_ccas_transaction_master set Region=@Region,TransactionDate=@TransactionDate,
                      CreditCardNo=@CreditCardNo,BaseAmount=@BaseAmount,BIN=@BIN,CardBrand=@CardBrand,
                      IssuingBank=@IssuingBank,TypeofCard=@TypeofCard,CategoryofCard=@CategoryofCard,IssuingCountryCode=@IssuingCountryCode,
                      LastUpdateDate=@LastUpdateDate,UpdatedBy= @UpdatedBy where SerialNo=@SerialNo AND StoreCode=@StoreCode AND DocumentNo=@DocumentNo";
            
            if (model.StoreCode.Substring(0, 1) == "1")
                ls_region = "HK";
            else if (model.StoreCode.Substring(0, 1) == "2")
                ls_region = "MO";
            else if (model.StoreCode.Substring(0, 1) == "6")
                ls_region = "CN";
            else if (model.StoreCode.Substring(0, 1) == "7")
                ls_region = "SG";
            else
                ls_region = "";


            string TransactionDate = FormatDate(model.TransactionDate);
            Dictionary<string, object> paramsValue = new Dictionary<string, object>() { 
                                                        {"Region",ls_region??""},
                                                        {"SerialNo",model.SerialNo??""},
                                                        {"StoreCode",model.StoreCode??"" },
                                                        {"TransactionDate", DateTime.Parse(TransactionDate)},
                                                        { "DocumentNo", model.DocumentNo??""},
                                                        { "CreditCardNo", model.CreditCardNo??"" },
                                                        { "BaseAmount", model.BaseAmount },
                                                        { "BIN", model.BIN??""},
                                                        {"CardBrand", model.CardBrand??""},
                                                        { "IssuingBank", model.IssuingBank??""},
                                                        { "TypeofCard", model.TypeofCard??"" },
                                                        { "CategoryofCard", model.CategoryofCard??"" },
                                                        { "IssuingCountryCode", model.IssuingCountryCode??""},
                                                        { "CreatedDate", DateTime.Now },
                                                        { "LastUpdateDate", DateTime.Now },
                                                        {"UpdatedBy",UserID }};

            ResultModel resultModel = new ResultModel();
            TransactionFileDA transactionFileDA = null;
            try
            {
                transactionFileDA = new TransactionFileDA();
                //object[] paramsValue = new object[]{  model.Region??"", model.TransactionDate??"",model.CreditCardNo??"",model.BaseAmount??"",model.BIN??"",model.CardBrand??"",
                //    model.IssuingBank??"",model.TypeofCard??"",model.CategoryofCard??"",model.IssuingCountryCode??"",
                //    DateTime.Now, UserID,model.SerialNo??"",model.StoreCode??"",model.DocumentNo??""};
                int rel = transactionFileDA.UpdateTransactionFile(ls_sql, paramsValue);
                if (rel > 0)
                {
                    resultModel.Affected = rel;
                }
                else
                {
                    resultModel.IsSuccess = false;
                    resultModel.Exception = "failed";
                }
            }
            catch (Exception ex)
            {
                resultModel.IsSuccess = false;
                resultModel.Exception = ex.Message;
            }
            finally
            {
                if (transactionFileDA != null)
                    transactionFileDA.CloseConnection();
            }
            return resultModel;
        }

        public virtual ResultModel DeleteTransactionFileByCode(string StoreCode, string SerialNo, string DocumentNo)
        {
            ResultModel resultModel = new ResultModel();
            TransactionFileDA transactionFileDA = null;
            try
            {
                transactionFileDA = new TransactionFileDA();

                Dictionary<string, object> paramsValue = new Dictionary<string, object>() { 
                                                        {"SerialNo",SerialNo.Trim ()},
                                                        {"StoreCode",StoreCode.Trim () },
                                                        { "DocumentNo", DocumentNo.Trim ()}};

                int rel = transactionFileDA.Delete(paramsValue);
                if (rel > 0)
                {
                    resultModel.Affected = rel;
                }
                else
                {
                    resultModel.IsSuccess = false;
                    resultModel.Exception = "failed";
                }
            }
            catch (Exception ex)
            {
                resultModel.IsSuccess = false;
                resultModel.Exception = ex.Message;
            }
            finally
            {
                if (transactionFileDA != null)
                    transactionFileDA.CloseConnection();
            }
            return resultModel;
        }

        public virtual PagingModel List(string StoreCode, string SerialNo, string DocumentNo, int pageSize, int pageIndex)
        {
            TransactionFileDA transactionFileDA = null;
            PagingModel dt = new PagingModel();
            try
            {
                transactionFileDA = new TransactionFileDA();
                dt = transactionFileDA.GetTransactionFileByPage(StoreCode, SerialNo, DocumentNo, new PagingModel() { PageIndex = pageIndex, PageSize = pageSize });
            }
            finally
            {
                if (transactionFileDA != null)
                    transactionFileDA.CloseConnection();
            }
            return dt;
        }

        public string FormatDate(string TransactionDate)
        {
            string trsdate = null; ;
            if (TransactionDate!=null)
                trsdate = TransactionDate.Substring(0, 4) + "/" + TransactionDate.Substring(4, 2) + "/" + TransactionDate.Substring(6, 2);


            return trsdate;
        }

    }
}
