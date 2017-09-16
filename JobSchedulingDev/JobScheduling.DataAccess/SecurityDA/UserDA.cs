using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JobScheduling.DBCommon;
using JobScheduling.DBCommon.dao;
using JobScheduling.Entity.CommModel;
using JobScheduling.Model.CommModel;

namespace JobScheduling.DataAccess.SecurityDA
{
    public class UserDA : Repository
    {

        /// <summary>
        /// New User
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public int NewUser(Dictionary<string, object> paramValues)
        {
            var am = GetSQLModel("t_ccas_user", paramValues);

            return Template.Execute(am.InsertSQL.ToString(), paramValues);
        }


        /// <summary>
        /// Edit User
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public int EditUser(Dictionary<string, object> paramValues, string UserID)
        {
            SQLModel am = GetSQLModel("t_ccas_user", paramValues);

            if (!String.IsNullOrEmpty(UserID))
            {
                am.UpdateSQL.Append(" UserID=@UserID ");
                paramValues.Add("@UserID", UserID);
            }

            return Template.Execute(am.UpdateSQL.ToString(), paramValues);
        }


        /// <summary>
        /// Get Users
        /// </summary>
        /// <param name="whereDate"></param>
        public DataTable GetUsers(string userID)
        {
            StringBuilder SelectSQL = new StringBuilder("select * from t_ccas_user where IsDeleted = 0 ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(userID))
            {
                SelectSQL.Append(" and UserID = @UserID");
                pvs.Add("@UserID", userID);
            }

            DataTable dt = Template.Query(SelectSQL.ToString(), pvs);

            return dt.Rows.Count > 0 ? dt : null;
        }


        /// <summary>
        /// Get Users
        /// </summary>
        /// <param name="whereDate"></param>
        public DataTable GetUsersForName(string groupID, string userName)
        {
            StringBuilder SelectSQL = new StringBuilder("select UID,UserID,UserName from t_ccas_user where IsDeleted = 0 and Active =1  ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(userName))
            {
                SelectSQL.Append(" and UserName like @UserName ESCAPE '/' ");
                pvs.Add("@UserName", "%" + userName + "%");
            }
            if (!string.IsNullOrEmpty(groupID))
            {
                SelectSQL.Append(" and UID not in(select UserUID from tr_ccas_user_group where GroupID = @GroupID)");
                pvs.Add("@GroupID", groupID);
            }


            DataTable dt = Template.Query(SelectSQL.ToString(), pvs);

            return dt.Rows.Count > 0 ? dt : null;
        }




        /// <summary>
        /// Get Users
        /// </summary>
        /// <param name="whereDate"></param>
        public DataTable GetUsersForGroup(string groupID, string userName)
        {
            StringBuilder SelectSQL = new StringBuilder(@"select t2.UID,t2.UserName,t2.UserID from tr_ccas_user_group t1 , t_ccas_user t2 
where t1.GroupID = @GroupID and  t1.UserUID = t2.UID and t2.IsDeleted = 0 and t2.Active =1 ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(groupID))
            {
                pvs.Add("@GroupID", groupID);
            }
            if (!string.IsNullOrEmpty(userName) && userName.Trim() !="%")
            {
                SelectSQL.Append(" and t2.UserName like @UserName ESCAPE '/' ");
                pvs.Add("@UserName", "%" + userName + "%");
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
        public PagingModel GetUserByPage(string userID ,string userName, PagingModel pm)
        {
            int totalCount = 0;

            StringBuilder SelectSQL = new StringBuilder("select UserID,UserID as 'User ID',UserName,Active from t_ccas_user where IsDeleted = 0  ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(userID))
            {
                SelectSQL.Append(" and UserID like @UserID ESCAPE '/' ");
                pvs.Add("@UserID", "%" + userID + "%");
            }
            if (!string.IsNullOrEmpty(userName))
            {
                SelectSQL.Append(" and UserName like @UserName ESCAPE '/' ");
                pvs.Add("@UserName", "%" + userName + "%");
            }

            SelectSQL.Append(" order by LastModifiedTime desc ,CreatedTime desc ");

            DataTable dt = Template.QueryByPage(SelectSQL.ToString(), pvs, pm.PageSize, pm.PageIndex, out totalCount);

            pm.DataTable = dt;
            pm.TotalCount = totalCount;

            return pm;
        }   



    }
}
