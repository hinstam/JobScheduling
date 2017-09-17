using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using JobScheduling.DBCommon;
using JobScheduling.DataAccess.CommonDA;
using JobScheduling.Model.MasterModel;

namespace JobScheduling.DataAccess.MasterDA
{
    public class DistrictDA:Repository
    {
        private const string DISTRICT_SELECT = @"select DistrictID,DistrictCode,DistrictDescription from t_ccas_district where 1=1";

        private const string DISTRICT_REGION_SELECT = @"select DistrictCode,DistrictDescription,CountryDescription from t_ccas_district a,t_ccas_country b where 1=1 and a.countryid=b.countryid";

        public DistrictM RelateregionRowObject(DataRow row)
        {
            DistrictM model = null;
            if (row != null)
            {
                model = new DistrictM();
                model.DistrictCode = row["DistrictCode"] == DBNull.Value ? null : (string)row["DistrictCode"];
                model.DistrictDescription = row["DistrictDescription"] == DBNull.Value ? null : (string)row["DistrictDescription"];
                model.RegionDescription = row["CountryDescription"] == DBNull.Value ? null : (string)row["CountryDescription"];
          }
            return model;
        }


        public DataSet GetDistrictList(string RegionIDs)
        {
            string sqlConnectionStr = ConfigurationManager.ConnectionStrings["JobSchedulingEntities"].ConnectionString;
            string where = string.Empty;
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(RegionIDs))
            {
                string regionIDList =InParamDA.SqlInParamList(RegionIDs, "@countryid", ref sqlParamList);
                where += " and countryid in (" + regionIDList + ")";
            }
            where += " order by districtdescription";

            SqlParameter[] paramList = sqlParamList.ToArray();
            using (DataSet ds = SQLHelper.ExecuteDataset(sqlConnectionStr, CommandType.Text, DISTRICT_SELECT + where, paramList))
            {
                return ds;
            }
        }

        //public DataSet GetDistrictByID(string districtid)
        //{
        //    string sqlConnectionStr = ConfigurationManager.ConnectionStrings["JobSchedulingEntities"].ConnectionString;
        //    string where = string.Empty;
        //    List<SqlParameter> sqlParamList = new List<SqlParameter>();
        //    if (!string.IsNullOrEmpty(districtid))
        //    {
                
        //        where += " and districtid=@districtid";
        //        sqlParamList.Add(new SqlParameter("@districtid",Guid.Parse(districtid)));
        //    }

        //    SqlParameter[] paramList = sqlParamList.ToArray();
        //    using (DataSet ds = SQLHelper.ExecuteDataset(sqlConnectionStr, CommandType.Text, DISTRICT_REGION_SELECT + where, paramList))
        //    {
        //        return ds;
        //    }
        //}

        public DataRow GetDistrictByID(string districtid)
        {
            string sql = DISTRICT_REGION_SELECT + " and districtid=@districtid";
            DataTable dt = Template.Query(sql, new string[] { "@districtid" }, new object[] { districtid });
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }
    }


}
