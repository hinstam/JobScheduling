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
    public class BinFileBL : Business
    {
        public ResultModel NewBinFile(BinFileM model)
        {
            ResultModel resultModel = new ResultModel();
            BinFileDA binFileDA = null;

            try
            {
                binFileDA = new BinFileDA();

                if (binFileDA.GetBinFileByCode(model.BIN) != null)
                {
                    resultModel.IsSuccess = false;
                    resultModel.Exception = "The BIN code has been saved!";
                    return resultModel;
                }
                object[] paramsValue = new object[] { "M", model.BIN, model.CardBrand??"", model.IssuingBank??"", model.TypeofCard??"",
                model.CategoryofCard??"",model.IssuingCountryISOA2Code??"",DateTime.Now, DateTime.Now, UserID };
                int rel = binFileDA.NewBinFile(paramsValue);
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
                if (binFileDA != null)
                    binFileDA.CloseConnection();
            }
            return resultModel;
        }

        /// <summary>
        /// Get BinFile by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public BinFileM GetBinFileByCode(string code)
        {
            BinFileDA binFileDA = null; 
            DataRow dr ;
            BinFileM model = null;
            try
            {
                binFileDA = new BinFileDA();
                dr = binFileDA.GetBinFileByCode(code);

                if (dr != null)
                    model = binFileDA.Row2Object(dr);
            }
            finally
            {
                if (binFileDA != null)
                    binFileDA.CloseConnection();
            }
            return model;
            
        }

        public ResultModel UpdateBinFile(BinFileM model)
        {
            ResultModel resultModel = new ResultModel();
            BinFileDA binFileDA = null;
            try
            {
                binFileDA = new BinFileDA();
                object[] paramsValue = new object[]{ "M", model.BIN, model.CardBrand??"", model.IssuingBank??"", model.TypeofCard??"",
                model.CategoryofCard??"",model.IssuingCountryISOA2Code??"",DateTime.Now, UserID};
                int rel = binFileDA.UpdateBinFile(paramsValue);
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
                if (binFileDA != null)
                    binFileDA.CloseConnection();
            }
            return resultModel;
        }

        public virtual ResultModel DeleteBinFileByCode(string code)
        {
            ResultModel resultModel = new ResultModel();
            BinFileDA binFileDA = null;
            try
            {
                binFileDA = new BinFileDA();

                object[] paramsValue = new object[] { code };
                int rel = binFileDA.Delete(paramsValue);
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
                if (binFileDA != null)
                    binFileDA.CloseConnection();
            }
            return resultModel;
        }

        public virtual PagingModel List(string code, int pageSize, int pageIndex)
        {
            BinFileDA binFileDA = null;
            PagingModel dt = new PagingModel();
            try
            {
                binFileDA = new BinFileDA();
                dt = binFileDA.GetBinFileByPage(code, new PagingModel() { PageIndex = pageIndex, PageSize = pageSize });
            }
            finally
            {
                if (binFileDA != null)
                    binFileDA.CloseConnection();
            }
            return dt;
        }

        
    }
}
