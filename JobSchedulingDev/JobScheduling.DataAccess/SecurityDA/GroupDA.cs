using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JobScheduling.Entity.CommModel;
using JobScheduling.Model.CommModel;

namespace JobScheduling.DataAccess.SecurityDA
{
    public class GroupDA : Repository
    {
        private const string TEXT_GetAllGroup = "SELECT * FROM t_ccas_group g where g.isdeleted=0 and operation=@operation";

        /// <summary>
        /// Get User Menu Information
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public DataTable GetAllGroup(string operation)
        {
            DataTable dt = Template.Query(TEXT_GetAllGroup, new string[] { "@operation" }, new string[] { operation });
            return dt;
        }


        /// <summary>
        /// Get all Group by page
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public PagingModel GetGroupByPage(string groupID, string groupName, string description ,string operation,PagingModel pm)
        {
            int totalCount = 0;

            StringBuilder SelectSQL = new StringBuilder("select GroupID,GroupID as 'Group ID',GroupName,NumOfUsers as 'No.of user',Description from t_ccas_group where IsDeleted = 0 and operation=@operation ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();
            pvs.Add("@operation", operation);

            if (!string.IsNullOrEmpty(groupID))
            {
                SelectSQL.Append(" and GroupID like @GroupID ESCAPE '/' ");
                pvs.Add("@GroupID", "%" + groupID + "%");
            }
            if (!string.IsNullOrEmpty(groupName))
            {
                SelectSQL.Append(" and GroupName like @GroupName ESCAPE '/' ");
                pvs.Add("@GroupName", "%" + groupName + "%");
            }
            if (!string.IsNullOrEmpty(description))
            {
                SelectSQL.Append(" and Description like @Description ESCAPE '/' ");
                pvs.Add("@Description", "%" + description + "%");
            }

            SelectSQL.Append(" order by LastModifiedTime desc ,CreatedTime desc ");

            DataTable dt = Template.QueryByPage(SelectSQL.ToString(), pvs, pm.PageSize, pm.PageIndex, out totalCount);

            pm.DataTable = dt;
            pm.TotalCount = totalCount;

            return pm;
        }



        /// <summary>
        /// New Group
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public int NewGroup(Dictionary<string, object> paramValues)
        {
            var am = GetSQLModel("t_ccas_group", paramValues);

            return Template.Execute(am.InsertSQL.ToString(), paramValues);
        }


        /// <summary>
        /// Edit Group
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public int EditGroup(Dictionary<string, object> paramValues, string groupID)
        {
            SQLModel am = GetSQLModel("t_ccas_group", paramValues);

            if (!String.IsNullOrEmpty(groupID))
            {
                am.UpdateSQL.Append(" GroupID=@GroupID ");
                paramValues.Add("@GroupID", groupID);
            }

            return Template.Execute(am.UpdateSQL.ToString(), paramValues);
        }


        /// <summary>
        /// Get Groups
        /// </summary>
        /// <param name="whereDate"></param>
        public DataTable GetGroups(string groupID,string operation)
        {
            StringBuilder SelectSQL = new StringBuilder("select * from t_ccas_group where IsDeleted = 0  and operation=@operation ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();
            pvs.Add("@operation", operation);

            if (!string.IsNullOrEmpty(groupID))
            {
                SelectSQL.Append(" and GroupID = @GroupID");
                pvs.Add("@GroupID", groupID);
            }

            DataTable dt = Template.Query(SelectSQL.ToString(), pvs);

            return dt.Rows.Count > 0 ? dt : null;
        }


        /// <summary>
        /// Get Groups
        /// </summary>
        /// <param name="whereDate"></param>
        public DataTable GetGroupsForName(string groupName, string operation)
        {
            StringBuilder SelectSQL = new StringBuilder("select GroupID,GroupName from t_ccas_group where IsDeleted = 0  and operation=@operation ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();
            pvs.Add("@operation", operation);

            if (!string.IsNullOrEmpty(groupName))
            {
                SelectSQL.Append(" and GroupName like @GroupName ESCAPE '/' ");
                pvs.Add("@GroupName", "%" + groupName + "%");
            }

            DataTable dt = Template.Query(SelectSQL.ToString(), pvs);

            return dt.Rows.Count > 0 ? dt : null;
        }



        /// <summary>
        /// New User Group
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public int NewUserGroup(Dictionary<string, object> paramValues)
        {
            var am = GetSQLModel("tr_ccas_user_group", paramValues);

            return Template.Execute(am.InsertSQL.ToString(), paramValues);
        }


        /// <summary>
        /// Del User Group
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public int DelUserGroup(Dictionary<string, object> paramValues)
        {
            StringBuilder DeleteSQL = new StringBuilder("delete from tr_ccas_user_group where GroupID =@GroupID and UserUID = @UserUID");

            return Template.Execute(DeleteSQL.ToString(), paramValues);
        }


        /// <summary>
        /// Get Groups
        /// </summary>
        /// <param name="whereDate"></param>
        public DataTable GetUserGroups(string groupID)
        {
            StringBuilder SelectSQL = new StringBuilder("select * from tr_ccas_user_group ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(groupID))
            {
                SelectSQL.Append(" where GroupID = @GroupID");
                pvs.Add("@GroupID", groupID);
            }

            DataTable dt = Template.Query(SelectSQL.ToString(), pvs);

            return dt.Rows.Count > 0 ? dt : null;
        }



    }
}
