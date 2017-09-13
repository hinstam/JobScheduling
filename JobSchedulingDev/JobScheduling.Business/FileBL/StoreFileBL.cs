using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobScheduling.Entity.CommModel;
using JobScheduling.Model.CommModel;
using JobScheduling.DataAccess.FileDA;
using JobScheduling.Model.FileModel;
using System.Data;
using JobScheduling.Business.CommonBL;

namespace JobScheduling.Business.FileBL
{
    public class StoreFileBL:Business
    {
        /// <summary>
        /// create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultModel NewStoreFile(StoreFileM model)
        {
            ResultModel resultModel = new ResultModel();
            StoreFileDA storeFileDA = null;

            try
            {
                storeFileDA = new StoreFileDA();

                if (storeFileDA.GetStoreFileByCode(model.StoreCode) != null)
                {
                    resultModel.IsSuccess = false;
                    resultModel.Exception = "The StoreCode has been saved!";
                    return resultModel;
                }
                object[] paramsValue = new object[] {Guid.NewGuid(),model.StoreCode, model.DistrictID,model.StoreFirstDescription,model.StoreLastDescription??string.Empty,model.Address,DateTime.Now, DateTime.Now, UserID };
                int rel = storeFileDA.NewStoreFile(paramsValue);
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
                if (storeFileDA != null)
                    storeFileDA.CloseConnection();
            }
            return resultModel;
        }

        /// <summary>
        /// update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultModel UpdateStoreFile(StoreFileM model)
        {
            ResultModel resultModel = new ResultModel();
            StoreFileDA storeFileDA = null;
            try
            {
                storeFileDA = new StoreFileDA();
                object[] paramsValue = new object[] { model.StoreID, model.StoreCode, model.DistrictID, model.StoreFirstDescription, model.StoreLastDescription ?? string.Empty, model.Address, DateTime.Now, DateTime.Now, UserID };
                int rel = storeFileDA.UpdateStoreFile(paramsValue);
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
                if (storeFileDA != null)
                    storeFileDA.CloseConnection();
            }
            return resultModel;
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual ResultModel DeleteStoreFileByCode(string storeCode)
        {
            ResultModel resultModel = new ResultModel();
            StoreFileDA storeFileDA = null;
            try
            {
                storeFileDA = new StoreFileDA();

                object[] paramsValue = new object[] { storeCode };
                int rel = storeFileDA.Delete(paramsValue);
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
                if (storeFileDA != null)
                    storeFileDA.CloseConnection();
            }
            return resultModel;
        }

        /// <summary>
        /// page list
        /// </summary>
        /// <param name="storeCode"></param>
        /// <param name="district"></param>
        /// <param name="description"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public virtual PagingModel List(string storeCode, string districtCode, string countryCode, string storeFirstDescription, string districtDescription, string address, int pageSize, int pageIndex)
        {
            StoreFileDA storeFileDA = null;
            PagingModel dt = new PagingModel();
            try
            {
                storeFileDA = new StoreFileDA();
                dt = storeFileDA.GetStoreFileByPage(storeCode, districtCode, countryCode, storeFirstDescription, districtDescription, address, new PagingModel() { PageIndex = pageIndex, PageSize = pageSize });
            }
            finally
            {
                if (storeFileDA != null)
                    storeFileDA.CloseConnection();
            }
            return dt;
        }

        public StoreFileM GetStoreFileByID(string storeid)
        {
            StoreFileDA storeFileDA = null;
            DataRow dr;
            StoreFileM model = null;
            try
            {
                storeFileDA = new StoreFileDA();
                dr = storeFileDA.GetStoreFileByID(storeid);

                if (dr != null)
                    model = storeFileDA.Row2Object(dr);
            }
            finally
            {
                if (storeFileDA != null)
                    storeFileDA.CloseConnection();
            }
            return model;

        }

        public StoreFileM GetStoreFileByCode(string storeCode)
        {
            StoreFileDA storeFileDA = null;
            DataRow dr;
            StoreFileM model = null;
            try
            {
                storeFileDA = new StoreFileDA();
                dr = storeFileDA.GetStoreFileByCode(storeCode);

                if (dr != null)
                    model = storeFileDA.Row2Object(dr);
            }
            finally
            {
                if (storeFileDA != null)
                    storeFileDA.CloseConnection();
            }
            return model;
        }

        public IList<StoreFileM> GetCountryList()
        {
            StoreFileDA storeFileDA = null;
            try
            {
                storeFileDA = new StoreFileDA();
                DataSet ds = storeFileDA.GetCountryList();
                IList<StoreFileM> storeList = ds.ToList<StoreFileM>();
                return storeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<StoreFileM> GetDistrictList(string countryid)
        {
            StoreFileDA storeFileDA = null;
            try
            {
                storeFileDA = new StoreFileDA();
                DataSet ds = storeFileDA.GetDistrictList(countryid);
                IList<StoreFileM> storeList = ds.ToList<StoreFileM>();
                return storeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }       
        }

        public IList<StoreFileM> GetStoreDescriptionList(string districtid)
        {
            StoreFileDA storeFileDA = null;
            try
            {
                storeFileDA = new StoreFileDA();
                DataSet ds = storeFileDA.GetStoreDescriptionList(districtid);
                IList<StoreFileM> storeList = ds.ToList<StoreFileM>();
                return storeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }       
 
        }

        public IList<StoreFileM> GetStoreDescriptionListByRegionIDs(string regionIDs)
        {
            StoreFileDA storeFileDA = null;
            try
            {
                storeFileDA = new StoreFileDA();
                DataSet ds = storeFileDA.GetStoreDescriptionListByRegionIDs(regionIDs);
                IList<StoreFileM> storeList = ds.ToList<StoreFileM>();
                return storeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }       
        }
    }
}
