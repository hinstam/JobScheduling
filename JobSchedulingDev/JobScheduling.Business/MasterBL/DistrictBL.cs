using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobScheduling.Model.MasterModel;
using JobScheduling.DataAccess.MasterDA;
using System.Data;
using JobScheduling.Business.CommonBL;

namespace JobScheduling.Business.MasterBL
{
    public class DistrictBL : Business
    {
        public IList<DistrictM> GetDistrictList(string regionIDs)
        {
            DistrictDA districtDA = null;
            try
            {
                districtDA = new DistrictDA();
                DataSet ds = districtDA.GetDistrictList(regionIDs);
                IList<DistrictM> districtList = ds.ToList<DistrictM>();
                return districtList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DistrictM GetDistrictByID(string districtid)
        {
            DistrictDA districtDA = null;
            DataRow dr;
            DistrictM model = null;
            try
            {
                districtDA = new DistrictDA();
                dr = districtDA.GetDistrictByID(districtid);

                if (dr != null)
                    model = districtDA.RelateregionRowObject(dr);
            }
            finally
            {
                if (districtDA != null)
                    districtDA.CloseConnection();
            }
            return model;

        }
    }
}
