using JobScheduling.DBCommon.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JobScheduling.DataAccess.SecurityDA
{
    public class AccessRightDA : Repository
    {
        private const string TEXT_GetAccessRightByControllerName = "SELECT * FROM v_ccas_useraccessright where moduleController=@moduleController and useruid=@useruid and operation=@operation ";
        //private const string TEXT_GetAllAccessRight = "SELECT m.code moduleCode,m.name moduleName,m.fathercode,m.isfathernode,a.code actionCode,a.name actionName from t_ccas_module m,t_ccas_action a,tr_ccas_module_action t where m.code=t.modulecode and t.actioncode=a.code and m.isdeleted='0' and m.isshow='1' order by m.code ";
        private const string TEXT_GetAllAccessRightByModuleCode = "SELECT A.* from t_ccas_action a,tr_ccas_module_action t where t.actioncode = a.code and t.moduleCode=@moduleCode order by a.showIndex";
        private const string TEXT_GetAccessRightByGroupID = "SELECT * FROM t_ccas_accessright t where t.isAllow=1 and t.groupid=@groupid";
        private const string TEXT_DeleteAccessRightByGroupID = "Delete FROM t_ccas_accessright where groupid=@groupid";
        private const string TEXT_InsertAccessRight = "Insert into t_ccas_accessright(ModuleCode,GroupID,ActionCode,ActionName,IsAllow,CreatedBy,CreatedTime) values (@moduleCode,@groupID,@actionCode,@actionName,@isAllow,@createBy,@createTime)";
        private const string TEXT_GetAllAction = "Select * FROM t_ccas_action ";

        public DataTable GetAccessRightByControllerName(string controllerName, string useruid, string operation)
        {
            DataTable dt = Template.Query(TEXT_GetAccessRightByControllerName, new string[] { "@moduleController", "@useruid", "@operation" }, new string[] { controllerName, useruid, operation });
            return dt;
        }

        public DataTable GetAccessRightByModuleCode(string moduleCode)
        {
            DataTable dt = Template.Query(TEXT_GetAllAccessRightByModuleCode, new string[] { "@moduleCode" }, new object[] { moduleCode });
            return dt;
        }

        public DataTable GetAccessRightByGroupID(string groupID)
        {
            DataTable dt = Template.Query(TEXT_GetAccessRightByGroupID, new string[] { "@groupid" }, new object[] { groupID });
            return dt;
        }

        public int DeleteAccessRightByGroupID(string groupID)
        {
            int result = Template.Execute(TEXT_DeleteAccessRightByGroupID, new string[] { "@groupid" }, new object[] { groupID });
            return result;
        }

        /// <summary>
        /// Insert accessright to database
        /// </summary>
        /// <param name="paramsValue">ModuleCode,GroupID,ActionCode,ActionName,IsAllow,CreatedBy,CreatedTime</param>
        /// <returns></returns>
        public int InsertAccessRight(params object[] paramsValue)
        {
            int result = Template.Execute(TEXT_InsertAccessRight, new string[] { "@moduleCode", "@groupID", "@actionCode", "@actionName", "@isAllow", "@createBy", "@createTime" }, paramsValue);
            return result;
        }

        public DataTable GetAllAction()
        {
            DataTable dt = Template.Query(TEXT_GetAllAction, null,null);
            return dt;
        }
    }
}
