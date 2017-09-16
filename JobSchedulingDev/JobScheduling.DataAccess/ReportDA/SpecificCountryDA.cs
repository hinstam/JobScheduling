using JobScheduling.Model.ReportModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JobScheduling.DataAccess.CommonDA;

namespace JobScheduling.DataAccess.ReportDA
{
    public class SpecificCountryDA : Repository
    {
        private string TEXT_GetSpecificCountrySum = @"SELECT IssuingBank ,t.CardBrand,count(*) as 'Transactions', sum(t.BaseAmount) as 'Amount'
            FROM t_ccas_transaction_master t {0} where 1=1 ";

        private string TEXT_GetSum = @"SELECT count(*) as 'TotalTransaction', sum(t.BaseAmount) as 'TotalAmount'
            FROM t_ccas_transaction_master t {0} where 1=1 ";

        public DataTable GetSpecificCountrySum(SpecificCountrySearchM model)
        {
            string groupSQL = " group by IssuingBank,CardBrand ";
            string orderSQL = " order by IssuingBank ";
            StringBuilder whereSelectSQL = new StringBuilder("");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(model.DistrictID) || !string.IsNullOrEmpty(model.StoreCode))
            {
                TEXT_GetSpecificCountrySum = string.Format(TEXT_GetSpecificCountrySum, "left join t_ccas_store_master s on t.storecode=s.storecode ");
            }
            else
            {
                TEXT_GetSpecificCountrySum = string.Format(TEXT_GetSpecificCountrySum,string.Empty);
            }

            if (!string.IsNullOrEmpty(model.DistrictID))
            {
                whereSelectSQL.Append(" and DistrictID =@DistrictID");
                pvs.Add("@DistrictID", model.DistrictID);
            }

            if (!string.IsNullOrEmpty(model.StoreCode))
            {
                whereSelectSQL.Append(" and t.StoreCode =@StoreCode");
                pvs.Add("@StoreCode", model.StoreCode);
            }


            if (!string.IsNullOrEmpty(model.CountryCode))
            {
                whereSelectSQL.Append(" and IssuingCountryCode =@CountryCode");
                pvs.Add("@CountryCode", model.CountryCode);
            }

            if (!string.IsNullOrEmpty(model.FromDate) && !string.IsNullOrEmpty(model.ToDate))
            {
                whereSelectSQL.Append(" and TransactionDate between @FromDate and @ToDate ");
                pvs.Add("@FromDate", model.FromDate);
                pvs.Add("@ToDate", model.ToDate);
            }
            else if (!string.IsNullOrEmpty(model.FromDate) && string.IsNullOrEmpty(model.ToDate))
            {
                whereSelectSQL.Append(" and TransactionDate >=@FromDate");
                pvs.Add("@FromDate", model.FromDate);
            }
            else if (!string.IsNullOrEmpty(model.FromDate) && string.IsNullOrEmpty(model.ToDate))
            {
                whereSelectSQL.Append(" and TransactionDate <=@ToDate");
                pvs.Add("@ToDate", model.FromDate);
            }

            //if (!string.IsNullOrEmpty(model.FromRegion) && !string.IsNullOrEmpty(model.ToRegion))
            //{
            //    whereSelectSQL.Append(" and Region between @FromRegion and @ToRegion ");
            //    pvs.Add("@FromRegion", model.FromDate);
            //    pvs.Add("@ToRegion", model.ToDate);
            //}
            //else if (!string.IsNullOrEmpty(model.FromDate) && string.IsNullOrEmpty(model.ToDate))
            //{
            //    whereSelectSQL.Append(" and Region >=@FromRegion");
            //    pvs.Add("@FromRegion", model.FromDate);
            //}
            //else if (string.IsNullOrEmpty(model.FromDate) && !string.IsNullOrEmpty(model.ToDate))
            //{
            //    whereSelectSQL.Append(" and Region <=@ToRegion");
            //    pvs.Add("@ToRegion", model.FromDate);
            //}
            //string region = "";
            //if (model.HongKong)
            //    region += ",'HK'";
            //if (model.Macau)
            //    region += ",'MO'";
            //if (model.China)
            //    region += ",'CN'";
            //if (model.Singapore)
            //    region += ",'SG'";
            //if (region != "")
            //{
            //    region = region.Substring(1);
            //    whereSelectSQL.Append(" and Region in (" + region + ")");
            //}
            //else
            //    whereSelectSQL.Append(" and Region is null ");


            if (!string.IsNullOrEmpty(model.RegionCodeList))
            {
                string regionCode = InParamDA.SqlInParamList2(model.RegionCodeList, "@regioncode", ref pvs);
                whereSelectSQL.Append(" and Region in (" + regionCode + ")");
            }
            //else
            //    whereSelectSQL.Append(" and Region is null ");

            string SelectSQL = TEXT_GetSpecificCountrySum+whereSelectSQL.ToString()+groupSQL+orderSQL;

            DataTable dt = Template.Query(SelectSQL, pvs);

            return dt;
        }

        public DataTable GetSum(SpecificCountrySearchM model)
        {
            StringBuilder whereSelectSQL = new StringBuilder("");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(model.DistrictID) || !string.IsNullOrEmpty(model.StoreCode))
            {
                TEXT_GetSum = string.Format(TEXT_GetSum, "left join t_ccas_store_master s on t.storecode=s.storecode ");
            }
            else
            {
                TEXT_GetSum = string.Format(TEXT_GetSum, string.Empty);
            }

            if (!string.IsNullOrEmpty(model.DistrictID))
            {
                whereSelectSQL.Append(" and DistrictID =@DistrictID");
                pvs.Add("@DistrictID", model.DistrictID);
            }

            if (!string.IsNullOrEmpty(model.StoreCode))
            {
                whereSelectSQL.Append(" and t.StoreCode =@StoreCode");
                pvs.Add("@StoreCode", model.StoreCode);
            }

            if (!string.IsNullOrEmpty(model.CountryCode))
            {
                whereSelectSQL.Append(" and IssuingCountryCode =@CountryCode");
                pvs.Add("@CountryCode", model.CountryCode);
            }

            if (!string.IsNullOrEmpty(model.FromDate) && !string.IsNullOrEmpty(model.ToDate))
            {
                whereSelectSQL.Append(" and TransactionDate between @FromDate and @ToDate ");
                pvs.Add("@FromDate", model.FromDate);
                pvs.Add("@ToDate", model.ToDate);
            }
            else if (!string.IsNullOrEmpty(model.FromDate) && string.IsNullOrEmpty(model.ToDate))
            {
                whereSelectSQL.Append(" and TransactionDate >=@FromDate");
                pvs.Add("@FromDate", model.FromDate);
            }
            else if (!string.IsNullOrEmpty(model.FromDate) && string.IsNullOrEmpty(model.ToDate))
            {
                whereSelectSQL.Append(" and TransactionDate <=@ToDate");
                pvs.Add("@ToDate", model.FromDate);
            }

            //if (!string.IsNullOrEmpty(model.FromRegion) && !string.IsNullOrEmpty(model.ToRegion))
            //{
            //    whereSelectSQL.Append(" and Region between @FromRegion and @ToRegion ");
            //    pvs.Add("@FromRegion", model.FromDate);
            //    pvs.Add("@ToRegion", model.ToDate);
            //}
            //else if (!string.IsNullOrEmpty(model.FromDate) && string.IsNullOrEmpty(model.ToDate))
            //{
            //    whereSelectSQL.Append(" and Region >=@FromRegion");
            //    pvs.Add("@FromRegion", model.FromDate);
            //}
            //else if (string.IsNullOrEmpty(model.FromDate) && !string.IsNullOrEmpty(model.ToDate))
            //{
            //    whereSelectSQL.Append(" and Region <=@ToRegion");
            //    pvs.Add("@ToRegion", model.FromDate);
            //}
            //string region = "";
            //if (model.HongKong)
            //    region += ",'HK'";
            //if (model.Macau)
            //    region += ",'MO'";
            //if (model.China)
            //    region += ",'CN'";
            //if (model.Singapore)
            //    region += ",'SG'";
            //if (region != "")
            //{
            //    region = region.Substring(1);
            //    whereSelectSQL.Append(" and Region in (" + region + ")");
            //}
            //else
            //    whereSelectSQL.Append(" and Region is null ");

            if (!string.IsNullOrEmpty(model.RegionCodeList))
            {
                string regionCode = InParamDA.SqlInParamList2(model.RegionCodeList, "@regioncode", ref pvs);
                whereSelectSQL.Append(" and Region in (" + regionCode + ")");
            }
            //else
            //    whereSelectSQL.Append(" and Region is null ");

            string SelectSQL = TEXT_GetSum + whereSelectSQL.ToString();

            DataTable dt = Template.Query(SelectSQL, pvs);

            return dt;
        }

        public List<SpecificCountryM> TableToList(DataTable data)
        {
            List<SpecificCountryM> result = null;
            if (data.Rows.Count > 0)
            {
                result = new List<SpecificCountryM>();
                foreach (DataRow item in data.Rows)
                {
                    SpecificCountryM plist = new SpecificCountryM();

                    plist.BankName = item["IssuingBank"].ToStringEx();
                    plist.CardBrand = item["CardBrand"].ToStringEx();

                    plist.Transactions = item["Transactions"].ToDecimal();
                    plist.Amount = item["Amount"].ToDecimal();

                    result.Add(plist);
                }
            }
            return result;
        }

        public DataTable GetFirstLastDate()
        {
            string ls_sql = "select max(TransactionDate) as max,min(TransactionDate) as min from t_ccas_transaction_master";
            DataTable dt = Template.Query(ls_sql, null);
            return dt;
        }
    }
}
