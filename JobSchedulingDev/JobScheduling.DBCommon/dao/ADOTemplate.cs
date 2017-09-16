using System;
using System.Collections.Generic;
using System.Data;
//using MySql.Data.MySqlClient;
//using System.Data.SqlClient;
//using System.Data.OracleClient;
//using Oracle.DataAccess;
//using Oracle.DataAccess.Client;
using JobScheduling.DBCommon;
using System.Reflection;    
using System.Data.Linq.Mapping;
using System.Text;
using System.Data.SqlClient;
namespace JobScheduling.DBCommon.dao
{
    public class ADOTemplate
    {
        private IDbConnection connection = null;

        public IDbConnection GetConnection()
        {
            string connectString = System.Configuration.ConfigurationManager.ConnectionStrings["EGCCASEntities"].ConnectionString;
            //string connectString = "data source=172.30.1.129,9299;initial catalog=EGScrum;user id=sa;password=Pw123456;"; 

            if (connection == null)
            {
                try
                {
                    
                    connection = new SqlConnection(connectString);
                    connection.Open();
                }
                catch (Exception ex)
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    throw;
                }
            }

            return connection;
        }

        public void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }



        /// <summary>
        /// Execute a SqlCommand (that using AOP to open and close connection). 
        /// </summary>
        /// <param name="sql">SqlCommand Text</param>
        /// <param name="paramNames">paramNames,none for null</param>
        /// <param name="paramValues">paramValues,none for null</param>
        /// <returns>DataTable</returns>
        public DataTable Query(string sql, String[] paramNames, Object[] paramValues) {
            //this.dbType = TransactionContext.get().dbType;
           
            //if (this.dbType == ADOTemplate.DB_TYPE_SQLSERVER)
            {

                SqlParameter[] parm = convert4Sqlserver(paramNames, paramValues);

                return SQLHelper.ExecuteDataset(
                    GetConnection() as SqlConnection, CommandType.Text, sql, parm
                    ).Tables[0];
            }
            //else if (this.dbType == ADOTemplate.DB_TYPE_MYSQL)
            //{

            //    MySqlParameter[] parm = convert4MySqlserver(paramNames, paramValues);

            //    return MySQLHelper.ExecuteDataset(
            //        TransactionContext.get().connection as MySqlConnection, CommandType.Text, sql, parm
            //        ).Tables[0];
            //}
            //else
            //{
            //    IDbDataParameter[] parm = convert4Oracle(paramNames, paramValues);

            //    return OracleHelper.ExecuteDataset(
            //        TransactionContext.get().connection, CommandType.Text, sql, parm
            //        ).Tables[0];
            //}

        }

        /// <summary>
        /// Execute a SqlCommand and return a DataTable by Paging (that using AOP to open and close connection). 
        /// </summary>
        /// <param name="sql">SqlCommand Text</param>
        /// <param name="paramNames">paramNames,none for null</param>
        /// <param name="paramValues">paramValues,none for null</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="totalCount">return the totalCount value</param>
        /// <returns>DataTable</returns>
        public DataTable QueryByPage(string sql, String[] paramNames, Object[] paramValues, int pageSize, int pageIndex,out int totalCount)
        {
            //this.dbType = TransactionContext.get().dbType;
            totalCount = 0;

            //if (this.dbType == ADOTemplate.DB_TYPE_SQLSERVER)
            //{

            SqlParameter[] parm = convert4Sqlserver(paramNames, paramValues);

            return SQLHelper.ExecuteDataset(
                GetConnection() as SqlConnection, CommandType.Text, sql, parm
                ).Tables[0];
            //}
            //else if (this.dbType == ADOTemplate.DB_TYPE_MYSQL)
            //{

                //MySqlParameter[] parm_temp = convert4MySqlserver(paramNames, paramValues);
                //if (parm_temp == null) parm_temp = new MySqlParameter[] { };

                ////Get total count of the result
                //string sqlCount = new StringBuilder(sql.Length + 20)
                //.Append("select count(*) as total from ( ")
                //.Append(sql)
                //.Append(" ) t")
                //.ToString();

                //DataTable dt = MySQLHelper.ExecuteDataset(
                //    TransactionContext.get().connection as MySqlConnection, CommandType.Text, sqlCount, parm_temp
                //    ).Tables[0];

                //if (dt.Rows.Count > 0)
                //{
                //    totalCount = Convert.ToInt32(dt.Rows[0][0]);  
                //}

                ////Search by paging
                //bool hasOffset = (pageIndex != 1);

                //sql = new StringBuilder(sql.Length + 20)
                //.Append(sql)
                //.Append(hasOffset ? " limit @x_rownum_from, @x_row_size" : " limit @x_row_size")
                //.ToString();

                //MySqlParameter[] parm;
                //if (hasOffset)
                //{
                //    parm = new MySqlParameter[parm_temp.Length + 2];
                //    parm_temp.CopyTo(parm,0);
                //    parm[parm_temp.Length] = new MySqlParameter("@x_rownum_from", ((pageIndex - 1) * pageSize));
                //    parm[parm_temp.Length + 1] = new MySqlParameter("@x_row_size", pageSize);
                //}
                //else
                //{
                //    parm = new MySqlParameter[parm_temp.Length + 1];
                //    parm_temp.CopyTo(parm, 0);
                //    parm[parm_temp.Length] = new MySqlParameter("@x_row_size", pageSize);
                //}

                //return MySQLHelper.ExecuteDataset(
                //    TransactionContext.get().connection as MySqlConnection, CommandType.Text, sql, parm
                //    ).Tables[0];
            //}
            //else
            //{
            //    IDbDataParameter[] parm = convert4Oracle(paramNames, paramValues);

            //    return OracleHelper.ExecuteDataset(
            //        TransactionContext.get().connection, CommandType.Text, sql, parm
            //        ).Tables[0];
            //}

        }


        public int Execute(string sql, String[] paramNames, Object[] paramValues)
        {
            //this.dbType = TransactionContext.get().dbType;

            //if (this.dbType == ADOTemplate.DB_TYPE_SQLSERVER)
            //{

            SqlParameter[] parm = convert4Sqlserver(paramNames, paramValues);

            return SQLHelper.ExecuteNonQuery(
                GetConnection() as SqlConnection, CommandType.Text, sql, parm
                );
            //}
            //else if (this.dbType == ADOTemplate.DB_TYPE_MYSQL)
            //{

                //MySqlParameter[] parm = convert4MySqlserver(paramNames, paramValues);

                //return MySQLHelper.ExecuteNonQuery(
                //    TransactionContext.get().connection as MySqlConnection, CommandType.Text, sql, parm
                //    );
            //}
            //else
            //{
            //    IDbDataParameter[] parm = convert4Oracle(paramNames, paramValues);
                
            //    return OracleHelper.ExecuteNonQuery(
            //        TransactionContext.get().connection, CommandType.Text, sql, parm
            //        );
                 
            //}

        }

        private SqlParameter[] convert4Sqlserver(String[] paramNames, Object[] paramValues)
        {
            if (paramNames == null) return null;

            var size = paramNames.Length;

            if (size == 0) return null;

            SqlParameter[] result = new SqlParameter[size];

            for (int i = 0; i < size; i++)
            {
                string name = paramNames[i];
                if (!name.StartsWith("@"))
                {
                    name = "@" + name;
                }

                result[i] = new SqlParameter(name, paramValues[i]);
            }

            return result;
        }
        
        //private MySqlParameter[] convert4MySqlserver(String[] paramNames, Object[] paramValues)
        //{
        //    if (paramNames == null) return null;

        //    var size = paramNames.Length;

        //    if (size == 0) return null;

        //    MySqlParameter[] result = new MySqlParameter[size];

        //    for (int i = 0; i < size; i++)
        //    {
        //        string name = paramNames[i];
        //        if (!name.StartsWith("@"))
        //        {
        //            name = "@" + name;
        //        }

        //        result[i] = new MySqlParameter(name, paramValues[i]);
        //    }

        //    return result;
        //}

        //private OracleParameter[] convert4Oracle(String[] paramNames, Object[] paramValues)
        //{
        //    if (paramNames == null) return null;

        //    var size = paramNames.Length;

        //    if (size == 0) return null;

        //    OracleParameter[] result = new OracleParameter[size];

        //    for (int i = 0; i < size; i++)
        //    {
        //        string name = paramNames[i];
        //        if (!name.StartsWith(":"))
        //        {
        //            name = ":" + name;
        //        }

        //        result[i] = new OracleParameter(name, paramValues[i]);
        //    }

        //    return result;
        //}


        #region  Jason Add code 2013-10-28

        /*因为String[] paramNames, Object[] paramValues 这两个数组最终会整合成一个 Parameter，分开写容易导致
         *两个数组数量或值不对应，一定要按顺序添加，因此这里使用 Dictionary<string, string> 代替
         *试行期间只修改MySql
        */


        public int Execute(string sql, Dictionary<string, object> paramValues)
        {
            //this.dbType = TransactionContext.get().dbType;

            //if (this.dbType == ADOTemplate.DB_TYPE_MYSQL)
            //{
            //    MySqlParameter[] parm = convert4MySqlserver(paramValues);

            //    return MySQLHelper.ExecuteNonQuery(
            //        TransactionContext.get().connection as MySqlConnection, CommandType.Text, sql, parm
            //        );
            //}

            SqlParameter[] parm = convert4Sqlserver(paramValues);

            return SQLHelper.ExecuteNonQuery(
                GetConnection() as SqlConnection, CommandType.Text, sql, parm
                );
            //return 0;
        }


        /// <summary>
        /// Execute a SqlCommand (that using AOP to open and close connection). 
        /// </summary>
        /// <param name="sql">SqlCommand Text</param>
        /// <param name="paramNames">paramNames,none for null</param>
        /// <param name="paramValues">paramValues,none for null</param>
        /// <returns>DataTable</returns>
        public DataTable Query(string sql, Dictionary<string, object> paramValues)
        {
            //this.dbType = TransactionContext.get().dbType;

            //if (this.dbType == ADOTemplate.DB_TYPE_MYSQL)
            //{
            //    MySqlParameter[] parm = convert4MySqlserver(paramValues);

            //    return MySQLHelper.ExecuteDataset(
            //        TransactionContext.get().connection as MySqlConnection, CommandType.Text, sql, parm
            //        ).Tables[0];
            //}
            SqlParameter[] parm = convert4Sqlserver(paramValues);

            return SQLHelper.ExecuteDataset(
                GetConnection() as SqlConnection, CommandType.Text, sql, parm
                ).Tables[0];
            //return null;
        }


        public DataTable QueryByPage(string sql, Dictionary<string, object> paramValues, int pageSize, int pageIndex, out int totalCount)
        {
            //this.dbType = TransactionContext.get().dbType;
            totalCount = 0;

            //if (this.dbType == ADOTemplate.DB_TYPE_MYSQL)
            //{

            //    MySqlParameter[] parm_temp = convert4MySqlserver(paramValues);
            //    if (parm_temp == null) parm_temp = new MySqlParameter[] { };

            //    //Get total count of the result
            //    string sqlCount = new StringBuilder(sql.Length + 20)
            //    .Append("select count(*) as total from ( ")
            //    .Append(sql)
            //    .Append(" ) t")
            //    .ToString();

            //    DataTable dt = MySQLHelper.ExecuteDataset(
            //        TransactionContext.get().connection as MySqlConnection, CommandType.Text, sqlCount, parm_temp
            //        ).Tables[0];

            //    if (dt.Rows.Count > 0)
            //    {
            //        totalCount = Convert.ToInt32(dt.Rows[0][0]);
            //    }

            //    //Search by paging
            //    bool hasOffset = (pageIndex != 1);

            //    sql = new StringBuilder(sql.Length + 20)
            //    .Append(sql)
            //    .Append(hasOffset ? " limit @x_rownum_from, @x_row_size" : " limit @x_row_size")
            //    .ToString();

            //    MySqlParameter[] parm;
            //    if (hasOffset)
            //    {
            //        parm = new MySqlParameter[parm_temp.Length + 2];
            //        parm_temp.CopyTo(parm, 0);
            //        parm[parm_temp.Length] = new MySqlParameter("@x_rownum_from", ((pageIndex - 1) * pageSize));
            //        parm[parm_temp.Length + 1] = new MySqlParameter("@x_row_size", pageSize);
            //    }
            //    else
            //    {
            //        parm = new MySqlParameter[parm_temp.Length + 1];
            //        parm_temp.CopyTo(parm, 0);
            //        parm[parm_temp.Length] = new MySqlParameter("@x_row_size", pageSize);
            //    }

            //    return MySQLHelper.ExecuteDataset(
            //        TransactionContext.get().connection as MySqlConnection, CommandType.Text, sql, parm
            //        ).Tables[0];
            //}
            //else if (this.dbType == ADOTemplate.DB_TYPE_SQLSERVER)
            {

                SqlParameter[] parm_temp = convert4Sqlserver(paramValues);
                if (parm_temp == null) parm_temp = new SqlParameter[] { };

                //Get total count of the result

                string sqlCount = "";
                int beginPos = 7; // start after select
                int nextFormPos = sql.IndexOf("from ", beginPos);
                int nextSelectPos = sql.IndexOf("select ", beginPos);

                while (nextSelectPos > 0 && nextFormPos > nextSelectPos)
                {
                    beginPos = nextFormPos + 4;

                    nextFormPos = sql.IndexOf("from ", beginPos);
                    nextSelectPos = sql.IndexOf("select ", beginPos);
                }

                string orderString = "";
                int endPos = sql.IndexOf("order by");

                if (endPos > 0)
                {
                    sqlCount = "SELECT COUNT(*) " + sql.Trim().Substring(nextFormPos, endPos - nextFormPos - 1);
                    orderString = sql.Trim().Substring(endPos);
                }
                else
                {
                    sqlCount = "SELECT COUNT(*) " + sql.Trim().Substring(nextFormPos);
                }

                DataTable dt = SQLHelper.ExecuteDataset(
                    GetConnection() as SqlConnection, CommandType.Text, sqlCount, parm_temp
                    ).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    totalCount = Convert.ToInt32(dt.Rows[0][0]);
                }

                //Search by paging
                bool hasOffset = (pageIndex != 1);

                sql = new SQLServerDialect().GetLimitSql(sql, pageIndex, pageSize);

                SqlParameter[] parm;
                if (hasOffset)
                {
                    parm = new SqlParameter[parm_temp.Length + 2];
                    parm_temp.CopyTo(parm, 0);
                    parm[parm_temp.Length] = new SqlParameter("@x_rownum_from", ((pageIndex - 1) * pageSize+1));
                    parm[parm_temp.Length + 1] = new SqlParameter("@x_rownum_to", pageSize * pageIndex);
                }
                else
                {
                    parm = new SqlParameter[parm_temp.Length + 1];
                    parm_temp.CopyTo(parm, 0);
                    parm[parm_temp.Length] = new SqlParameter("@x_rownum_to", pageSize);
                }

                return SQLHelper.ExecuteDataset(
                    GetConnection() as SqlConnection, CommandType.Text, sql, parm
                    ).Tables[0];
            }
            return null;
        }


        //private MySqlParameter[] convert4MySqlserver(Dictionary<string, object> paramValues)
        //{
        //    if (paramValues == null) return null;

        //    if (paramValues.Count == 0) return null;

        //    List<MySqlParameter> result = new List<MySqlParameter>();

        //    foreach (var item in paramValues)
        //    {
        //        string name = item.Key;

        //        if (!name.StartsWith("@"))
        //        {
        //            name = "@" + name;
        //        }
        //        result.Add(new MySqlParameter(name, item.Value));
        //    }
        //    return result.ToArray();
        //}

        private SqlParameter[] convert4Sqlserver(Dictionary<string, object> paramValues)
        {
            if (paramValues == null) return null;

            if (paramValues.Count == 0) return null;

            List<SqlParameter> result = new List<SqlParameter>();

            foreach (var item in paramValues)
            {
                string name = item.Key;

                if (!name.StartsWith("@"))
                {
                    name = "@" + name;
                }
                result.Add(new SqlParameter(name, item.Value));
            }
            return result.ToArray();
        }


        private int getAfterSelectInsertPoint(String sql)
        {
            String sqlLower = sql.ToLower();
            int selectIndex = sqlLower.IndexOf("select");
            int selectDistinctIndex = sqlLower.IndexOf("select distinct");
            return selectIndex + (selectDistinctIndex == selectIndex ? 15 : 6);
        }
        #endregion





    }


    public class DBUtil {
        /// <summary>
        /// 把Row类型数据，通过反射，转为指定类型的对象，
        /// 转换时，
        /// 1）创建objectType的实例object，遍历objectType中的所有属性
        /// 2）每一属性，先看有没有Column Attribute定义，
        ///       a)如果有，取出column's name，
        ///       b)若没有，则属性名
        ///    作为数据行字段查询名字（名字统一细写）
        /// 3）从数据行查出来的值，放于实例object中
        /// </summary>
        /// <param name="row">数据行</param>
        /// <generic name="T">返回的对象类型</generic>
        /// <returns>objectType指定类型的实例</returns>
        /// author=Edgar Ng
        public static T Row2Object<T>(DataRow row) where T:new()
        {
            T obj = new T() ;
            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {                
                string proName = prop.Name ;
                //if it has ColumAttribute,then get ColumnAttribute name ;
                object[] atCCAS1 = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (atCCAS1.Length > 0)//if true,it has ColumnAttribute
                {
                    ColumnAttribute colum = atCCAS1[0] as ColumnAttribute;
                    if (colum != null)
                    {
                        proName = colum.Name;
                    }
                }
                if (row.Table.Columns.Contains(proName))
                {
                    if (row[proName] != DBNull.Value)
                    prop.SetValue(obj,row[proName],null); 
                }              
            } 
            return obj;
        }
        

        /// <summary>
        /// 把Table中的数据，通过反射，转为指定类型的对象列表
        /// </summary>
        /// author=Edgar Ng
        /// <param name="DataTable">数据表</param>
        /// <generic name="T">返回列表中，元素的对象类型</generic>
        /// <returns>objectType指定类型的列表</returns>
        public static IList<T> Table2List<T>(DataTable table)where T:new()
        {
            IList<T> result=new List<T>(table.Rows.Count);            
            T obj = new T();            
            PropertyInfo[] props = obj.GetType().GetProperties();
            IList<Object[]> proNameList = getEntityName<T>(props, table);//Object[]:Object[]{PropertyInfo,string}
            foreach (DataRow row in table.Rows)
            {
                T t = new T();  //t is temp
                for (int i = 0; i < proNameList.Count;i++ )
                {                    
                    PropertyInfo prop=proNameList[i][0] as PropertyInfo;
                    string strColumnName = proNameList[i][1] as string;
                    if (row[strColumnName] != DBNull.Value)
                    prop.SetValue(t, row[strColumnName], null);
                }
                result.Add(t);
             }
            return result;
        }


        /// <summary>
        /// to support the function Table2List():
        /// </summary>
        /// author=Edgar Ng
        private static IList<Object[]> getEntityName<T>(PropertyInfo[] props ,DataTable dt)
        {
            IList<Object[]> result = new List<Object[]>();
            DataRow drTemp = dt.Rows[0];
            foreach (PropertyInfo prop in props)
            {
                string proName = prop.Name;
                object[] colAtt = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (colAtt.Length > 0)//if true,it has ColumnAttribute
                {
                    ColumnAttribute colum = colAtt[0] as ColumnAttribute;
                    if (colum != null)
                    {
                        proName = colum.Name;
                    }
                }
                if (drTemp.Table.Columns.Contains(proName))
                {
                    Object[] objTemp=new Object[]{prop,proName};
                    result.Add(objTemp);
                }
            }
            return result ;
        }


        /*
            public static String getCountSql(final String sql) {
    	
        String tempSql = sql.toLowerCase().trim() ;
        
        int beginPos = 7; // start after select
        int nextFormPos = tempSql.indexOf("from ", beginPos);
        int nextSelectPos = tempSql.indexOf("select ", beginPos);
        
        while (nextSelectPos >0 && nextFormPos > nextSelectPos) {
        	beginPos = nextFormPos + 4 ;

            nextFormPos = tempSql.indexOf("from ", beginPos);
            nextSelectPos = tempSql.indexOf("select ", beginPos);
        }
        
    	
        int endPos = tempSql.indexOf("order by");
        
        if (endPos > 0) {
            return "SELECT COUNT(*) " + sql.trim().substring(nextFormPos, endPos -1);
        } else {
            return "SELECT COUNT(*) " + sql.trim().substring(nextFormPos);
        }
    }*/
    }






}  



