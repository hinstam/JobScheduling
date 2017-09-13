using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobScheduling.DataAccess;
using System.Data;
using System.Data.SqlClient;
using JobScheduling.DataAccess.CommonDA;

namespace JobScheduling.DataAccess.MasterDA
{
    public class RegionDA : Repository
    {
        public DataTable GetRegionList(string regionIDs)
        {
            StringBuilder SelectSQL = new StringBuilder("select countryid,countrycode,countrydescription from t_ccas_country ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(regionIDs))
            {
                string regionIDList = InParamDA.SqlInParamList2(regionIDs,"@countryid",ref pvs);
                SelectSQL.Append("where 1=1 and countryid in ("+regionIDList+")");
            }
            //SelectSQL.Append(" order by description");
            DataTable dt = Template.Query(SelectSQL.ToString(), pvs);

            return dt.Rows.Count > 0 ? dt : null;
        }
    }
}
