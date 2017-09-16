using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

namespace JobScheduling.Business.CommonBL
{
    public static class DatasetToListBL
    {
        public static IList<T> ToList<T>(this DataTable dt)
        {
            if (dt == null)
                return null;

            var list = new List<T>();
            Type t = typeof(T);
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());

            foreach (DataRow item in dt.Rows)
            {
                T s = Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                    if (info != null)
                    {
                        if (!Convert.IsDBNull(item[i]))
                        {
                            info.SetValue(s, item[i], null);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }

        public static IList<T> ToList<T>(this DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0)
                return null;

            return ds.Tables[0].ToList<T>();
        }
    }
}
