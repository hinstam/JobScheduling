using JobScheduling.DataAccess;
using JobScheduling.Entity.CommModel;
using JobScheduling.Model.CommModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JobScheduling.DataAccess.MasterDA
{
    public class CountryDA : Repository
    {
        /// <summary>
        /// New Country
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public int NewCountry(Dictionary<string, object> paramValues)
        {
            var am = GetSQLModel("t_md_country", paramValues);

            return Template.Execute(am.InsertSQL.ToString(), paramValues);
        }


        /// <summary>
        /// Edit Country
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public int EditCountry(Dictionary<string, object> paramValues, string code)
        {
            SQLModel am = GetSQLModel("t_md_country", paramValues);

            if (!String.IsNullOrEmpty(code))
            {
                am.UpdateCCASSQL.Append(" code=@codeParm ");
                paramValues.Add("@codeParm", code);
            }

            return Template.Execute(am.UpdateCCASSQL.ToString(), paramValues);
        }

        /// <summary>
        /// Edit Country
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public int DelCountry( string code)
        {
            Dictionary<string, object> paramValues = new Dictionary<string, object>();
            SQLModel am = GetSQLModel("t_md_country", paramValues);

            if (!String.IsNullOrEmpty(code))
            {
                am.DeleteSQL.Append(" code=@codeParm ");
                paramValues.Add("@codeParm", code);
            }

            return Template.Execute(am.DeleteSQL.ToString(), paramValues);
        }


        /// <summary>
        /// Get Users
        /// </summary>
        /// <param name="whereDate"></param>
        public DataTable GetCountry(string code)
        {
            StringBuilder SelectSQL = new StringBuilder("select * from t_md_country ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(code))
            {
                SelectSQL.Append(" where code = @code");
                pvs.Add("@code", code);
            }

            DataTable dt = Template.Query(SelectSQL.ToString(), pvs);

            return dt.Rows.Count > 0 ? dt : null;
        }

        /// <summary>
        /// Get all user by page
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public PagingModel GetCountryByPage(string code, string description, PagingModel pm)
        {
            int totalCount = 0;

            StringBuilder SelectSQL = new StringBuilder("select Code,Code as 'Country Code',Description as 'Country Description' from t_md_country where 1=1  ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(code))
            {
                SelectSQL.Append(" and Code like @Code ESCAPE '/' ");
                pvs.Add("@Code", "%" + code + "%");
            }
            if (!string.IsNullOrEmpty(description))
            {
                SelectSQL.Append(" and Description like @Description ESCAPE '/' ");
                pvs.Add("@Description", "%" + description + "%");
            }

            SelectSQL.Append(" order by LastUpdateDate desc ,CreatedDate desc ");

            DataTable dt = Template.QueryByPage(SelectSQL.ToString(), pvs, pm.PageSize, pm.PageIndex, out totalCount);

            pm.DataTable = dt;
            pm.TotalCount = totalCount;

            return pm;
        }
        /// <summary>
        /// Insert table from file
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public int InsertTable(string sql, Dictionary<string, object> paramValues)
        {
            return Template.Execute(sql, paramValues);
        }

        public DataTable GetAllCountry(string code)
        {
            StringBuilder SelectSQL = new StringBuilder("select code,description from t_md_country ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();
         
            if (!string.IsNullOrEmpty(code))
            {
                SelectSQL.Append(" where Description like @code ESCAPE '/' ");
                pvs.Add("@code", "%" + code + "%");
            }
            SelectSQL.Append(" order by description");
            DataTable dt = Template.Query(SelectSQL.ToString(), pvs);

            return dt.Rows.Count > 0 ? dt : null;
        }

    }
}
