using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobScheduling.Entity.CommModel;
using System.Data;
using System.Configuration;
using JobScheduling.Model.FileModel;
using JobScheduling.DBCommon;
using System.Data.SqlClient;
using JobScheduling.DataAccess.CommonDA;

namespace JobScheduling.DataAccess.FileDA
{
    public class StoreFileDA:Repository
    {
        private const string STORE_LIST_SELECT = @"select a.storeid code, a.StoreCode,a.storelastdescription StoreDescription,c.CountryCode,c.CountryDescription,b.DistrictCode,b.DistrictDescription,a.Address from t_ccas_store_master a,t_ccas_district b,t_ccas_country c where 1=1 and a.districtid=b.districtid and b.countryid=c.countryid";

        private const string STORE_ITEM_SELECT = @"select a.storeid code, a.StoreCode,a.storelastdescription StoreDescription,c.CountryCode,c.CountryDescription,b.DistrictCode,b.DistrictDescription,a.Address,b.DistrictID from t_ccas_store_master a,t_ccas_district b,t_ccas_country c where 1=1 and a.districtid=b.districtid and b.countryid=c.countryid";
        
        private const string STORE_CREATE = @"insert into t_ccas_store_master(storeid,storecode,districtid,storefirstdescription,storelastdescription,address,CreateDate,LastUpdateDate,UpdateBy) 
values(@storeid,@storecode,@districtid,@storefirstdescription,@storelastdescription,@address,@CreateDate,@LastUpdateDate,@UpdateBy)";

        private const string STORE_UPDATE = @"update t_ccas_store_master set storecode=@storecode,districtid=@districtid,storefirstdescription=@storefirstdescription,storelastdescription=@storelastdescription,address=@address,CreateDate=@CreateDate,LastUpdateDate=@LastUpdateDate,UpdateBy=@UpdateBy where storeid=@storeid";
        private const string STORE_DELETE = @"delete from t_ccas_store_master where storeid=@storeid";

        private const string STORE_DISTRICT_SELECT = @"select DistrictID,DistrictCode,DistrictDescription from t_ccas_district where 1=1";
        private const string STORE_STOREDESCRIPTION_SELECT = @"select concat(StoreCode,'-',StoreLastDescription) StoreLastDescription,StoreCode from t_ccas_store_master where 1=1";
        private const string STORE_COUNTRY_SELECT=@"select CountryCode,CountryDescription from t_ccas_country order by CountryCode";
        private const string STORE_REGIONID_SELECT = @"select concat(StoreCode,'-',StoreLastDescription) StoreLastDescription,StoreCode from t_ccas_store_master a,t_ccas_district b where 1=1 and a.districtid=b.districtid";


        /// <summary>
        /// storeFile model
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public StoreFileM Row2Object(DataRow row)
        {
            StoreFileM model = null;
            if (row != null)
            {
                model = new StoreFileM();
                model.StoreID = Guid.Parse(row["code"].ToString());
                model.StoreCode = row["StoreCode"] == DBNull.Value ? null : (string)row["StoreCode"];
                model.StoreLastDescription = row["StoreDescription"] == DBNull.Value ? null : (string)row["StoreDescription"];
                model.CountryCode = row["CountryCode"] == DBNull.Value ? null : (string)row["CountryCode"];
                model.CountryDescription = row["CountryDescription"] == DBNull.Value ? null : (string)row["CountryDescription"];
                model.DistrictCode = row["DistrictCode"] == DBNull.Value ? null : (string)row["DistrictCode"];
                model.DistrictDescription = row["DistrictDescription"] == DBNull.Value ? null : (string)row["DistrictDescription"];
                model.Address = row["Address"] == DBNull.Value ? null : (string)row["Address"];
                model.DistrictID = row["DistrictID"] == DBNull.Value ? Guid.Empty : Guid.Parse(row["DistrictID"].ToString());

            }
            return model;
        }

        /// <summary>
        /// get detail
        /// </summary>
        /// <param name="storeCode"></param>
        /// <returns></returns>
        public DataRow GetStoreFileByID(string storeid)
        {
            string sql = STORE_ITEM_SELECT + " and storeid=@storeid";
            DataTable dt = Template.Query(sql, new string[] { "@storeid" }, new object[] { storeid });
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }

        public DataRow GetStoreFileByCode(string storeCode)
        {
            string sql = STORE_ITEM_SELECT + " and storeCode=@storeCode";
            DataTable dt = Template.Query(sql, new string[] { "@storeCode" }, new object[] { storeCode });
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }

        /// <summary>
        /// create
        /// </summary>
        /// <param name="paramsValue"></param>
        /// <returns></returns>
        public int NewStoreFile(object[] paramsValue)
        {
            return Template.Execute(STORE_CREATE, new string[] { "@storeid", "@storecode", "@districtid", "@StoreFirstDescription", "@StoreLastDescription", "@address", "@CreateDate", "@LastUpdateDate", "@UpdateBy" }, paramsValue);
        }

        /// <summary>
        /// update
        /// </summary>
        /// <param name="paramsValue"></param>
        /// <returns></returns>
        public int UpdateStoreFile(object[] paramsValue)
        {
            return Template.Execute(STORE_UPDATE, new string[] { "@storeid", "@storecode", "@districtid", "@StoreFirstDescription", "@StoreLastDescription", "@address", "@CreateDate", "@LastUpdateDate", "@UpdateBy" }, paramsValue);
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="paramsValue"></param>
        /// <returns></returns>
        public int Delete(object[] paramsValue)
        {
            return Template.Execute(STORE_DELETE, new string[] { "@storeid" }, paramsValue);
        }

        /// <summary>
        /// page
        /// </summary>
        /// <param name="storeCode"></param>
        /// <param name="district"></param>
        /// <param name="description"></param>
        /// <param name="pm"></param>
        /// <returns></returns>
        public PagingModel GetStoreFileByPage(string storeCode, string districtCode, string countryCode, string storeFirstDescription, string districtDescription, string address, PagingModel pm)
        {
            int totalCount = 0;

            StringBuilder SelectSQL = new StringBuilder();

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(storeCode))
            {
                SelectSQL.Append(" and storecode like @storecode");
                pvs.Add("@storecode", "%" + storeCode + "%");
            }
            if (!string.IsNullOrEmpty(districtCode))
            {
                SelectSQL.Append(" and b.districtCode like @districtCode");
                pvs.Add("@districtCode", "%"+districtCode+"%");
            }

            if (!string.IsNullOrEmpty(address))
            {
                SelectSQL.Append(" and address like @address");
                pvs.Add("@address","%"+address+"%");
            }
            if (!string.IsNullOrEmpty(countryCode))
            {
                SelectSQL.Append(" and c.countryCode like @countryCode");
                pvs.Add("@countryCode", "%"+countryCode+"%");
            }
            if (!string.IsNullOrEmpty(districtDescription))
            {
                SelectSQL.Append(" and b.districtDescription like @districtDescription");
                pvs.Add("@districtDescription", "%" + districtDescription + "%");
            }

            if (!string.IsNullOrEmpty(storeFirstDescription))
            {
                SelectSQL.Append(" and storelastDescription like @storelastDescription");
                pvs.Add("@storelastDescription", "%" + storeFirstDescription + "%");
            }

            SelectSQL.Append(" order by a.storecode");

            DataTable dt = Template.QueryByPage(STORE_LIST_SELECT+SelectSQL.ToString(), pvs, pm.PageSize, pm.PageIndex, out totalCount);

            pm.DataTable = dt;
            pm.TotalCount = totalCount;

            return pm;
        }

        public DataSet GetCountryList()
        {
            string sqlConnectionStr = ConfigurationManager.ConnectionStrings["JobSchedulingEntities"].ConnectionString;
            using (DataSet ds = SQLHelper.ExecuteDataset(sqlConnectionStr, CommandType.Text, STORE_COUNTRY_SELECT))
            {
                return ds;
            }
        }

        public DataRow GetDistrictByID(string districtID)
        {
            string sql = STORE_DISTRICT_SELECT + " and districtid=@districtid";
            DataTable dt = Template.Query(sql, new string[] { "@districtid" }, new object[] { districtID });
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }

        public DataSet GetDistrictList(string countryid)
        {
             string sqlConnectionStr=ConfigurationManager.ConnectionStrings["JobSchedulingEntities"].ConnectionString;
             List<SqlParameter> sqlParamList = new List<SqlParameter>();
             string where = string.Empty;
             if (!string.IsNullOrEmpty(countryid))
             {
                 where += " and countryid=@countryid";
                 sqlParamList.Add(new SqlParameter("@countryid", Guid.Parse(countryid)));
             }
             where += " order by districtdescription";

             SqlParameter[] paramList = sqlParamList.ToArray();
            using (DataSet ds = SQLHelper.ExecuteDataset(sqlConnectionStr, CommandType.Text, STORE_DISTRICT_SELECT+where,paramList))
            {
                return ds;
            }
        }

        public DataSet GetStoreDescriptionList(string districtid)
        {
             string sqlConnectionStr=ConfigurationManager.ConnectionStrings["JobSchedulingEntities"].ConnectionString;
             List<SqlParameter> sqlParamList = new List<SqlParameter>();
             string where = string.Empty;
             if (!string.IsNullOrEmpty(districtid))
            {
                where += " and  districtid=@districtid";
             sqlParamList.Add(new SqlParameter("@districtid",Guid.Parse(districtid)));
            }

            where+=" order by storecode";

            SqlParameter[] paramList = sqlParamList.ToArray();
            using (DataSet ds = SQLHelper.ExecuteDataset(sqlConnectionStr, CommandType.Text, STORE_STOREDESCRIPTION_SELECT+where, paramList))
            {
                return ds;
            }
        }

        public DataSet GetStoreDescriptionListByRegionIDs(string regionIDs)
        {
             string sqlConnectionStr=ConfigurationManager.ConnectionStrings["JobSchedulingEntities"].ConnectionString;
             List<SqlParameter> sqlParamList = new List<SqlParameter>();
             string where = string.Empty;
             if (!string.IsNullOrEmpty(regionIDs))
            {
                string regionIDList = InParamDA.SqlInParamList(regionIDs,"@regionid",ref sqlParamList);
                where += " and  b.countryid in ("+regionIDList+")";
            }

            where+=" order by storecode";

            SqlParameter[] paramList = sqlParamList.ToArray();
            using (DataSet ds = SQLHelper.ExecuteDataset(sqlConnectionStr, CommandType.Text, STORE_REGIONID_SELECT+where, paramList))
            {
                return ds;
            }
        }
    }
}
