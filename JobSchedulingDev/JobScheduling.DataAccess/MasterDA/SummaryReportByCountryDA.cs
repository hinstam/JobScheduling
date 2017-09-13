using JobScheduling.Entity.CommModel;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.MasterModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JobScheduling.DataAccess.CommonDA;

namespace JobScheduling.DataAccess.MasterDA
{
    public class SummaryReportByCountryDA : Repository
    {
        private const string TEXT_GetSummary = @"SELECT case when IssuingCountryCode is null then 'Unidentified' else IssuingCountryCode  end as country,
                 count(*) as 'no_of_tx', 
	             sum(t.BaseAmount) as 'amtcnt',
	             case when IssuingCountryCode is null then '1' else '0'  end as orderIndex
                 FROM t_ccas_transaction_master t where 1=1 ";

        private const string TEXT_GetTotalSum = @"SELECT count(*) as 'TotalTransaction', sum(t.BaseAmount) as 'TotalAmount'
            FROM t_ccas_transaction_master t where 1=1 ";

        public SummaryReportByCountryM Row2Object(DataRow row)
        {
            SummaryReportByCountryM model = null;
            if (row != null)
            {
                model = new SummaryReportByCountryM();
                model.Country = row["Country"].ToString();
                model.NumberOfTrx = long.Parse(row["no_of_tx"].ToString() ?? "0");
                //model.TrxPercent = decimal.Parse (row["per"].ToStringEx()??"0");
                model.Amount = decimal.Parse(row["amtcnt"].ToStringEx() ?? "0");
                //model.AmtPercent = decimal.Parse(row["Amtper"].ToStringEx() ?? "0");
            
            }
            return model;
        }


        //public DataTable GetSummary(string fm_date,string to_date,string fm_region,string to_region)
        //{

        //    String SelectSQL ;
        //    Dictionary<string, object> pvs = new Dictionary<string, object>();
        //    SelectSQL="select DISTINCT a.IssuingCountryCode as country, ";
        //    SelectSQL+=" count(*) as no_of_tx, ";
        //    SelectSQL+=" case ";
        //    SelectSQL+=" when c.total<>0 then  (CONVERT (decimal(19,8),convert(decimal(19,8),count(*))/c.total)) ";
        //    SelectSQL+=" end as per, ";
        //    SelectSQL+=" sum(a.BaseAmount)AS amtcnt, ";
        //    SelectSQL+=" case ";
        //    SelectSQL+=" when c.sumBsAmt<>0 then(CONVERT (decimal(19,8), convert(decimal(19,2),sum(a.BaseAmount))/c.sumBsAmt)) ";
        //    SelectSQL+=" end as Amtper, ";
        //    SelectSQL+=" '1' as orderstr ";
        //    SelectSQL+=" from t_ccas_transaction_master a ";
        //    SelectSQL+=" join t_md_country b on  a.IssuingCountryCode=b.Code ";
        //    SelectSQL+=" join (select DISTINCT a.IssuingCountryCode,(select count(*) from t_ccas_transaction_master ) as total,(select Sum(BaseAmount) from t_ccas_transaction_master ) as sumBsAmt from t_ccas_transaction_master a ";
        //    SelectSQL+=" GROUP BY IssuingCountryCode) c ";
        //    SelectSQL+=" on a.IssuingCountryCode=c.IssuingCountryCode";
        //    SelectSQL += " where 1=1 ";
        //  //  SelectSQL += " GROUP BY a.IssuingCountryCode,c.total,c.sumBsAmt";

        //    if (!string.IsNullOrEmpty(fm_date) && (fm_date!=""))
        //    {
        //        SelectSQL += " and TransactionDate >= @fm_date ";
        //        pvs.Add("@fm_date", "" + fm_date + "");
                
        //    }
        //    if (!string.IsNullOrEmpty(to_date) && (to_date != ""))
        //    {
        //        SelectSQL += " and TransactionDate <= @to_date ";
        //        //SelectSQL.Append(" and BankNameChinese like @BankNameChinese ESCAPE '/' ");
        //        pvs.Add("@to_date", "" + to_date + "");
        //    }


        //    if (!string.IsNullOrEmpty(fm_region) && (fm_region != ""))
        //    {
        //        if (!string.IsNullOrEmpty(to_region) && (to_region != ""))
        //        {
        //            SelectSQL += " and (Region between @fm_region and @to_region) ";
        //            //SelectSQL.Append(" and BankNameChinese like @BankNameChinese ESCAPE '/' ");
        //            pvs.Add("@fm_region", "" + fm_region.ToUpper() + "");
        //            pvs.Add("@to_region", "" + to_region.ToUpper() + "");
        //        }
        //        else
        //        {
        //            SelectSQL += " and Region >= @fm_region ";
        //            pvs.Add("@fm_region", "" + fm_region.ToUpper() + "");
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(to_region) && (to_region != "") && (string.IsNullOrEmpty(fm_region) || (fm_region == "")))
        //    {
        //        SelectSQL += " and Region <= @to_region ";
        //        //SelectSQL.Append(" and BankNameChinese like @BankNameChinese ESCAPE '/' ");
        //        pvs.Add("@to_region", "" + to_region.ToUpper() + "");
        //    }
        //    SelectSQL += " GROUP BY a.IssuingCountryCode,c.total,c.sumBsAmt";


        //    SelectSQL += " union ";
        //    SelectSQL+="select 'Unidentified' as country, ";
        //    SelectSQL+="count(*) as no_of_tx, ";
        //    SelectSQL+="case ";
        //    SelectSQL+="when (select count(*) from t_ccas_transaction_master) <>0 then convert(decimal(19,4),count(*)/convert(decimal(19,4),(select count(*) from t_ccas_transaction_master))) ";
        //    SelectSQL+="else 0 ";
        //    SelectSQL+="end as per, ";
        //    SelectSQL+="case  ";
        //    SelectSQL+="when sum(BaseAmount) is null then 0   ";
        //    SelectSQL+="end AS amtcnt, ";
        //    SelectSQL+="case when (select sum(BaseAmount) from t_ccas_transaction_master) <>0 then sum(BaseAmount)/convert(decimal(19,2),(select sum(BaseAmount) from t_ccas_transaction_master)) ";
        //    SelectSQL+="else 0 ";
        //    SelectSQL+="end as Amtper , ";
        //    SelectSQL+="'2' as orderstr  ";
        //    SelectSQL+="from t_ccas_transaction_master where IssuingCountryCode is null  ";          
                     
        //    //  SelectSQL += " GROUP BY a.IssuingCountryCode,c.total,c.sumBsAmt";

        //    if (!string.IsNullOrEmpty(fm_date) && (fm_date != ""))
        //    {
        //        SelectSQL += " and TransactionDate >= @fm_date_null ";
        //        pvs.Add("@fm_date_null", "" + fm_date + "");

        //    }
        //    if (!string.IsNullOrEmpty(to_date) && (to_date != ""))
        //    {
        //        SelectSQL += " and TransactionDate <= @to_date_null ";
        //        //SelectSQL.Append(" and BankNameChinese like @BankNameChinese ESCAPE '/' ");
        //        pvs.Add("@to_date_null", "" + to_date + "");
        //    }
        //    SelectSQL += " order by orderstr, country ";

        //    DataTable dt = Template.Query(SelectSQL, pvs);
        //    return dt;
        //}

        public DataTable GetSummary(PamReportM model)
        {
            string groupSQL = " group by IssuingCountryCode order by orderIndex, country";
            StringBuilder whereSelectSQL = new StringBuilder("");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(model.fm_date) && (model.fm_date != ""))
            {
                whereSelectSQL.Append(" and TransactionDate >= @fm_date ");
                pvs.Add("@fm_date", "" + model.fm_date + "");

            }
            if (!string.IsNullOrEmpty(model.to_date) && (model.to_date != ""))
            {
                whereSelectSQL.Append(" and TransactionDate <= @to_date ");
                //SelectSQL.Append(" and BankNameChinese like @BankNameChinese ESCAPE '/' ");
                pvs.Add("@to_date", "" + model.to_date + "");
            }
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

            string SelectSQL = TEXT_GetSummary + whereSelectSQL.ToString() + groupSQL;

            DataTable dt = Template.Query(SelectSQL, pvs);

            return dt;
        }


        public DataTable GetTotalSum(PamReportM model)
        {
            StringBuilder whereSelectSQL = new StringBuilder("");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(model.fm_date) && (model.fm_date != ""))
            {
                whereSelectSQL.Append(" and TransactionDate >= @fm_date ");
                pvs.Add("@fm_date", "" + model.fm_date + "");

            }
            if (!string.IsNullOrEmpty(model.to_date) && (model.to_date != ""))
            {
                whereSelectSQL.Append(" and TransactionDate <= @to_date ");
                pvs.Add("@to_date", "" + model.to_date + "");
            }

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
            else
                whereSelectSQL.Append(" and Region is null ");

            string SelectSQL = TEXT_GetTotalSum + whereSelectSQL.ToString();

            DataTable dt = Template.Query(SelectSQL, pvs);

            return dt;
        }

        public List<SummaryReportByCountryM> Table2List(DataTable table)
        {
            List<SummaryReportByCountryM> list = new List<SummaryReportByCountryM>();
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    list.Add(Row2Object(row));
                }
            }
            return list;
        }
        public DataTable GetFirstLastDate()
        {
            string ls_sql = "select max(TransactionDate) as max,min(TransactionDate) as min from t_ccas_transaction_master";
            DataTable dt = Template.Query(ls_sql, null);
            return dt;
        }
    }
}
