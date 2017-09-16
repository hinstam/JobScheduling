using JobScheduling.DBCommon.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
//using MySql.Data.MySqlClient;
using JobScheduling.Model.CommModel;

namespace JobScheduling.DataAccess
{
    public class Repository
    {
        private ADOTemplate _template;
        public ADOTemplate Template
        {
            get
            {
                if (_template == null)
                {
                    _template = new ADOTemplate();
                }
                return _template;
            }          
        }


        public void CloseConnection()
        {
            Template.CloseConnection();
        }

        /// <summary>
        /// Get AccessModel
        /// </summary>
        /// <param name="tableName">table Name</param>
        /// <param name="obj"> Insert , Update or Delete Data , type for Anonymous </param>
        /// <returns></returns>
        protected SQLModel GetSQLModel(string tableName, Dictionary<string, object> paramValues = null)
        {
            SQLModel model = new SQLModel();

            StringBuilder paramStr = new StringBuilder();
            StringBuilder valueStr = new StringBuilder();
            StringBuilder setStr = new StringBuilder();

            if (paramValues != null)
            {
                //允许插入空值
                //var pvs = paramValues.Where(z => z.Value != null);
                var pvs = paramValues;

                foreach (var item in pvs)
                {

                    string name = item.Key;
                    object value = item.Value;

                    //允许插入空值
                    //if (value == null)
                    //    continue;

                    if (item.Key == pvs.Last().Key)
                    {
                        paramStr.Append(name);
                        valueStr.Append("@" + name);
                        setStr.Append(name + "=@" + name);
                    }
                    else
                    {
                        paramStr.Append(name + ',');
                        valueStr.Append("@" + name + ',');
                        setStr.Append(name + "=@" + name + ',');
                    }
                }
            }

            model.InsertSQL = new StringBuilder(string.Format("insert into {0} ({1}) values ({2})", tableName, paramStr, valueStr));

            model.DeleteSQL = new StringBuilder(string.Format("delete {0} where ", tableName));

            model.UpdateSQL = new StringBuilder(string.Format("update {0} set {1} where IsDeleted = 0 and ", tableName, setStr));

            model.UpdateCCASSQL = new StringBuilder(string.Format("update {0} set {1} where  ", tableName, setStr));

            model.DeleteCCASSQL = new StringBuilder(string.Format("update {0} set IsDeleted = 1 where IsDeleted = 0 and ", tableName));
            return model;
        }
    }


    #region 方便table类型转换时精简写法。 Jason 2013-11-07

    public static class ConvertHelper
    {

        public static Decimal? ToDecimal(this Object obj)
        {
            if (obj == DBNull.Value || obj == null) return null;

            return Convert.ToDecimal(obj);
        }

        public static DateTime? ToDateTime(this Object obj)
        {
            if (obj == DBNull.Value || obj == null) return null;

            return Convert.ToDateTime(obj);
        }

        public static Int32? ToInt32(this Object obj)
        {
            if (obj == DBNull.Value || obj == null) return null;

            return Convert.ToInt32(obj);
        }

        public static string ToStringEx(this Object obj)
        {
            if (obj == DBNull.Value || obj == null) return null;

            return obj.ToString();
        }

    }

    #endregion
}
