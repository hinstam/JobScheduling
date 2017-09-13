using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace JobScheduling.DataAccess.CommonDA
{
    public static class InParamDA
    {
        /// <summary>
        /// 字符串数组参数化
        /// </summary>
        /// <param name="strArray"></param>
        /// <param name="paramName"></param>
        /// <param name="sqlParamList"></param>
        /// <returns></returns>
        public static string SqlInParamList(string arrayStr, string paramName, ref List<SqlParameter> sqlParamList)
        {
            string inStr = string.Empty;
            //sqlParamList = new List<SqlParameter>();
            string[] strArray = arrayStr.Split(',');
            for (int i = 0; i < strArray.Length; i++)
            {
                inStr += paramName + (i + 1).ToString() + ",";
                sqlParamList.Add(new SqlParameter(paramName + (i + 1).ToString(), strArray[i]));
            }
            inStr = inStr.Substring(0, inStr.Length - 1);
            return inStr;
        }

        public static string SqlInParamList2(string arrayStr, string paramName, ref Dictionary<string, object> pvs)
        {
            string inStr = string.Empty;
            //sqlParamList = new List<SqlParameter>();
            string[] strArray = arrayStr.Split(',');
            for (int i = 0; i < strArray.Length; i++)
            {
                inStr += paramName + (i + 1).ToString() + ",";
                pvs.Add(paramName + (i + 1).ToString(), strArray[i]);
            }
            inStr = inStr.Substring(0, inStr.Length - 1);
            return inStr;
        }
    }
}
