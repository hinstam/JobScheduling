using Castle.DynamicProxy;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
//using System.Data.SqlClient;
//using System.Data.OracleClient;
//using MySql.Data.MySqlClient;
//using Oracle.DataAccess;
//using Oracle.DataAccess.Client;

namespace EG.Utility.DBCommon.dao
{
    /// <summary>
    /// 事务管理AOP，
    /// </summary>
    public class TransactionAOP : IInterceptor
    {
        private string connectString = null;
        private bool useTransaction = false; 

        public TransactionAOP()
        {
        }
        
        public TransactionAOP(bool _useTransaction)
        {
            useTransaction = _useTransaction;
        }

        public TransactionAOP(string connectString)
        {
            this.connectString = connectString;
        }

        public void Intercept(IInvocation invocation)
        {
            IDbTransaction transaction = null;
            IDbConnection connection = null;

            short dbType = short.Parse(System.Configuration.ConfigurationManager.AppSettings["dbType"]);
            // 从类中指定，
            // 从方法中指定
            if (connectString == null)
            {
                connectString = System.Configuration.ConfigurationManager.ConnectionStrings["EGCCASEntities"].ConnectionString;
            }

            using (connection = getConnection(dbType, connectString))
            {
                try
                {
                    connection.Open();

                    string methodName = invocation.MethodInvocationTarget.Name.ToLower();
                    if (useTransaction)
                    {
                        transaction = connection.BeginTransaction();
                    }

                    new TransactionContext(connection, transaction, dbType);

                    invocation.Proceed();

                    if (transaction != null)
                    {
                        transaction.Commit();
                    }
                }
                catch(Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }

                    throw;
                }
                finally
                {
                    if (connection != null && ConnectionState.Open == connection.State) {
                        connection.Close();
                    }
                }
            }

        }

        /// <summary>
        /// get connection Object by dbType & connect String
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectString"></param>
        /// <returns></returns>
        private IDbConnection getConnection(short dbType, string connectString)
        { 
            //if (dbType == ADOTemplate.DB_TYPE_ORACLE) {
            //    return new OracleConnection(connectString);
            //} else if (dbType == ADOTemplate.DB_TYPE_SQLSERVER) {
                return new SqlConnection(connectString) ;
            //} else {
            //    return new MySqlConnection(connectString);
            //}
        }

        /// <summary>
        /// TransactionAOP的代理类创建，被代理对象具有数据库连接及事务管理功能，
        /// 但对象的方法，必须声明为virtual，方法中，可以直接使用ADOTemplate。
        /// 
        /// <code>
        /// BizBO bizBo = TransactionAOP.newInstance(typeof(BizBO)) as BizBO;
        /// 
        /// bizBo.add();//BizBO中，add方法，需要声明为virtual。
        /// 
        /// </code>
        /// 
        /// </summary>
        /// <param name="classType">被代理对象的类型</param>
        /// <returns>对象的代理</returns>
        public static object newInstance(Type classType,bool useTransaction=false)
        {
            return new ProxyGenerator().CreateClassProxy(classType, new TransactionAOP(useTransaction));
        }
    }

    public class AOPUtil
    {
        public static object newInstance(Type classType)
        {
            return new ProxyGenerator().CreateClassProxy(classType, new TransactionAOP());
        }

        public static object newInstance(Type classType,string connectionString)
        {
            return new ProxyGenerator().CreateClassProxy(classType, new TransactionAOP(connectionString));
        }
    }

    public class TransactionContext {
        private static ThreadLocal<TransactionContext> local = new ThreadLocal<TransactionContext>();

        public TransactionContext(IDbConnection connection, IDbTransaction transaction, short dbType)
        {
            this.connection = connection;
            this.transaction = transaction;
            this.dbType = dbType;

            local.Value = this;
        }
        public static TransactionContext get()
        {
            return local.Value;
        }

        public IDbConnection connection { get; set; }
        public IDbTransaction transaction { get; set; }
        public short dbType { get; set; }
    }
}
