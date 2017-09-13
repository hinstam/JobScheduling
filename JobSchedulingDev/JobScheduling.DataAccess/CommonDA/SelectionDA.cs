using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JobScheduling.DataAccess.CommonDA
{
    public class SelectionDA : Repository
    {
        /// <summary>
        /// get table selectlist
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="isContainDeleted"></param>
        /// <param name="colsName"></param>
        /// <returns></returns>
        public DataTable GetList(string tableName, bool isContainDeleted, params string[] colsName)
        {
            string sqlText = null;
            if (isContainDeleted)
            {
                sqlText = "select @params from @tablename where IsDeleted=@IsDeleted;";
            }
            else
            {
                sqlText = "select @params from @tablename;";
            }
            sqlText = sqlText.Replace("@params", string.Join(",", colsName)).Replace("@tablename", tableName);
            DataTable dt = Template.Query(sqlText, new string[] { "@IsDeleted" }, new object[] { 0 });
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public DataTable GetListEx(string tableName, bool isContainDeleted, params string[] colsName)
        {
            string sqlText = null;
            if (isContainDeleted)
            {
                sqlText = "select @params from @tablename where IsDelete=@IsDelete;";
            }
            else
            {
                sqlText = "select @params from @tablename;";
            }
            sqlText = sqlText.Replace("@params", string.Join(",", colsName)).Replace("@tablename", tableName);
            DataTable dt = Template.Query(sqlText, new string[] { "@IsDelete" }, new object[] { 0 });
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        /// <summary>
        /// get table selectlist
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="paramValues"></param>
        /// <param name="paramNames"></param>
        /// <param name="colsName"></param>
        /// <returns></returns>
        public DataTable GetList(string tableName, object[] paramValues, string[] paramNames, params string[] colsName)
        {
            string sqlText = "select @params from @tablename ";
            string[] paramNamesWhere = new string[paramNames.Length];
            for (int i = 0; i < paramNames.Length; i++)
            {
                paramNamesWhere[i] = paramNames[i] + "=@" + paramNames[i];
            }
            string whereText = string.Join(" and ", paramNamesWhere);
            if (!string.IsNullOrEmpty(whereText))
            {
                sqlText = sqlText + " where " + whereText;
            }
            sqlText = sqlText.Replace("@params", string.Join(",", colsName)).Replace("@tablename", tableName);
            DataTable dt = Template.Query(sqlText, paramNames, paramValues);
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }



        #region jasonyiu 2013-11-20


        public DataTable GetDataTable(string tableName,string orderBy, bool isAll, params string[] colsName)
        {
            StringBuilder SelectSQL = new StringBuilder(string.Format(" select {0} from {1} ", string.Join(",", colsName), tableName));

            if (!isAll)
            {
                SelectSQL.Append(string.Format(" where IsDeleted=0 "));
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                SelectSQL.Append(string.Format(" order by {0} ", orderBy));
            }

            return Template.Query(SelectSQL.ToString(), null);
        }



        public DataTable GetDataTable(string tableName, Dictionary<string, object> whereParam, params string[] colsName)
        {
            StringBuilder SelectSQL = new StringBuilder(string.Format(" select {0} from {1} ", string.Join(",", colsName), tableName));

            if (whereParam.Count() > 0)
            {
                SelectSQL.Append(" where ");

                foreach (var item in whereParam)
                {
                    SelectSQL.AppendFormat(" {0}=@{0} {1} ", item.Key, item.Key == whereParam.Last().Key ? string.Empty : " and ");
                }
            }
            return Template.Query(SelectSQL.ToString(), whereParam);
        }




        #endregion



    }
}
