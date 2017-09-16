using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobScheduling.Model.MasterModel;
using System.Data.SqlClient;
using System.Data;
using JobScheduling.DataAccess.MasterDA;

namespace JobScheduling.Business.MasterBL
{
    public class RegionBL : Business
    {

        public IList<RegionM> GetRegionList(string regionIDs)
        {
            IList<RegionM> regionList = new List<RegionM>();
            DataTable dt = new DataTable();
            RegionDA regionDA = null;
            try
            {
                regionDA = new RegionDA();
                dt = regionDA.GetRegionList(regionIDs);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RegionM regionM = new RegionM();
                        regionM.RegionID = Guid.Parse(dr["CountryID"].ToString());
                        regionM.RegionCode = dr["CountryCode"] != null ? dr["CountryCode"].ToString() : string.Empty;
                        regionM.RegionDescription = dr["CountryDescription"] != null ? dr["CountryDescription"].ToString() : string.Empty;
                        regionList.Add(regionM);
                    }
                }
            }
            finally
            {
                if (regionDA != null)
                    regionDA.CloseConnection();
            }
            return regionList;
        }


    }
}
