using JobScheduling.DBCommon.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.SessionState;
using System.Web;

namespace JobScheduling.Business
{
    public class Business
    {

        #region Login Session

        private HttpSessionState _Session;

        protected HttpSessionState Session
        {
            get
            {
                if (_Session == null)
                {
                    _Session = HttpContext.Current.Session;
                    _Session.Timeout = 21600;
                }
                return _Session;
            }
        }


        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            set { Session["UserName"] = value; }
            get
            {
                if (Session["UserName"] != null)
                {
                    return Session["UserName"].ToString();
                }
                return null;
            }
        }


        /// <summary>
        /// UserUID
        /// </summary>
        public string UserUID
        {
            set { Session["UserUID"] = value; }
            get
            {
                if (Session["UserUID"] != null)
                {
                    return Session["UserUID"].ToString();
                }
                return null;
            }
        }


        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID
        {
            set { Session["UserID"] = value; }
            get
            {
                if (Session["UserID"] != null)
                {
                    return Session["UserID"].ToString();
                }
                return null;
            }
        }


        /// <summary>
        /// Operation Code
        /// </summary>
        public string Operation
        {
            set { Session["Operation"] = value; }
            get
            {
                if (Session["Operation"] != null)
                {
                    return Session["Operation"].ToString();
                }
                return null;
            }
        }

        /// <summary>
        /// Operation Code
        /// </summary>
        public string OperationName
        {
            set { Session["OperationName"] = value; }
            get
            {
                if (Session["OperationName"] != null)
                {
                    return Session["OperationName"].ToString();
                }
                return null;
            }
        }

        /// <summary>
        /// Local Currency
        /// </summary>
        public string LocalCurrency
        {
            set { Session["LocalCurrency"] = value; }
            get
            {
                if (Session["LocalCurrency"] != null)
                {
                    return Session["LocalCurrency"].ToString();
                }
                return null;
            }
        }


        #endregion

        #region Project Log Book

        ////jason 2013-11-22
        ////二级子页面一般都有ProjectUID，因此不需要保存Session          
        ///// <summary>
        ///// Project UID
        ///// </summary>
        //public string ProjectUID
        //{
        //    set { Session["ProjectUID"] = value; }
        //    get
        //    {
        //        if (Session["ProjectUID"] != null)
        //        {
        //            return Session["ProjectUID"].ToString();
        //        }
        //        return null;
        //    }
        //}

        #endregion


    }




}
