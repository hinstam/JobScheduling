using JobScheduling.Common;
using JobScheduling.DataAccess.SecurityDA;
using JobScheduling.Entity.SecurityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JobScheduling.Business.SecurityBL
{
    public class AccessRightBL : Business
    {
        /// <summary>
        /// Get accessright by controller name 
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public  Dictionary<string, bool> GetAccessRightByControllerName(string controllerName ,out string moduleBelong)
        {
            controllerName = controllerName.ToLower().Trim();
            moduleBelong = null;

            //Get user access right
            AccessRightDA accessRightDA = null; 
            DataTable dtAR = new DataTable();
            Dictionary<string, bool> dicAR = new Dictionary<string, bool>();
            try
            {
                accessRightDA = new AccessRightDA();
                dtAR = accessRightDA.GetAccessRightByControllerName(controllerName, UserUID, Operation);
                
                for (int i = 0; i < dtAR.Rows.Count; i++)
                {
                    DataRow dr = dtAR.Rows[i];
                    if (dr["ActionName"] != null && dr["IsAllow"] != null)
                        dicAR[dr["ActionName"].ToString().ToLower()] = Convert.ToBoolean(dr["IsAllow"]);

                    if (dr["ModuleBelong"] != null)
                        moduleBelong = dr["ModuleBelong"].ToString();

                }

            }
            finally
            {
                if (accessRightDA != null)
                    accessRightDA.CloseConnection();
            }

            return dicAR;
        }

        /// <summary>
        /// Get all module accessright by groupID (if groupID is null,get the first one in groupList)
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public  AccessRightModel GetAllAccessRight(string groupID)
        {
            string selectedGroupID = "";
            string selectedGroupName = "";

            AccessRightModel arModel = new AccessRightModel();
            //get group list
            List<GroupM> groupList = GetAllGroup(groupID,out selectedGroupID,out selectedGroupName);
            arModel.GroupList = groupList;
            arModel.SelectedGroupID = selectedGroupID;
            arModel.SelectedGroupName = selectedGroupName;

            //get module access right
            if (groupList.Count > 0)
            {
                List<string> allowAccessActionList = GetModuleAccessRightByGroupID(selectedGroupID);//get allow action of the selected group
                List<ModuleRightModel> moduleRightList = GetAllModule(allowAccessActionList);//get all module and all accessright info
                arModel.ModuleRightList = moduleRightList;
            }

            return arModel;
        }

        /// <summary>
        /// Get all goup info
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="selectedGroupID"></param>
        /// <param name="selectedGroupName"></param>
        /// <returns></returns>
        private List<GroupM> GetAllGroup(string groupID,out string selectedGroupID,out string selectedGroupName)
        {
            selectedGroupID = "";
            selectedGroupName = "";

            bool getGroupFisrstElement=false;
            if (groupID == null)
                getGroupFisrstElement = true;

            List<GroupM> groupList = new List<GroupM>();

            GroupDA groupDA = null;
            DataTable dt = new DataTable();
            try
            {
                groupDA = new GroupDA();
                dt = groupDA.GetAllGroup(Operation);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    GroupM group = new GroupM();
                    group.GroupID = CommUtil.ConvertObjectToString(dr["GroupID"]);
                    group.GroupName = CommUtil.ConvertObjectToString(dr["GroupName"]);
                    groupList.Add(group);

                    if (i == 0 && getGroupFisrstElement)
                    {
                        selectedGroupID = group.GroupID;
                        selectedGroupName = group.GroupName;
                    }

                    if (group.GroupID.Equals(groupID))
                    {
                        selectedGroupID = group.GroupID;
                        selectedGroupName = group.GroupName;
                    }
                }
            }
            finally
            {
                if (groupDA != null)
                    groupDA.CloseConnection();
            }
            return groupList;
        }

        /// <summary>
        /// Get the module allow action by groupid 
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        private List<string> GetModuleAccessRightByGroupID(string groupID)
        {
            List<string> moduleARList = new List<string>();
            AccessRightDA accessRightDA = null;
            DataTable dt = new DataTable();
            try
            {
                accessRightDA = new AccessRightDA();
                dt = accessRightDA.GetAccessRightByGroupID(groupID);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string allowAccessModuleAction = CommUtil.ConvertObjectToString(dr["ModuleCode"]) + "_" + CommUtil.ConvertObjectToString(dr["ActionCode"]);
                    moduleARList.Add(allowAccessModuleAction);
                }
            }
            finally
            {
                if (accessRightDA != null)
                    accessRightDA.CloseConnection();
            }
            return moduleARList;
        }

        /// <summary>
        /// Get all module accessright
        /// </summary>
        /// <param name="allowAccessActionList"></param>
        /// <returns></returns>
        private List<ModuleRightModel> GetAllModule(List<string> allowAccessActionList)
        {
            ModuleDA moduleDA = null; 
            AccessRightDA accessRightDA =null; 

            List<ModuleRightModel> moduleRightList = new List<ModuleRightModel>();
            DataTable dtAllModule = new DataTable();
            try
            {
                moduleDA = new ModuleDA();
                accessRightDA = new AccessRightDA();
                dtAllModule = moduleDA.GetAllLevel1Module();
                for (int i = 0; i < dtAllModule.Rows.Count; i++)
                {
                    DataRow dr = dtAllModule.Rows[i];
                    if (dr["code"] != null)
                    {
                        ModuleRightModel mrModel = FillModuleRightModel(dr, allowAccessActionList, accessRightDA);

                        if (CommUtil.ConvertObjectToBool(dr["IsFatherNode"]))
                        {
                            mrModel.SubModule = GetSubModule(moduleDA, accessRightDA, allowAccessActionList, mrModel.ModuleCode, 1);
                        }
                        moduleRightList.Add(mrModel);
                    }
                }
            }
            finally
            {
                if (accessRightDA!=null)
                    accessRightDA.CloseConnection();
                if (moduleDA != null)
                    moduleDA.CloseConnection();
            }
            return moduleRightList;
        }

        /// <summary>
        /// get sub module accessright
        /// </summary>
        /// <param name="allowAccessActionList"></param>
        /// <param name="moduleCode"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        private List<ModuleRightModel> GetSubModule(ModuleDA moduleDA,AccessRightDA accessRightDA,List<string> allowAccessActionList,string moduleCode, int deep)
        {
            List<ModuleRightModel> moduleRightList = new List<ModuleRightModel>();

            DataTable dtAllModule = moduleDA.GetModuleByFatherCode(moduleCode);
            for (int i = 0; i < dtAllModule.Rows.Count; i++)
            {
                DataRow dr = dtAllModule.Rows[i];
                if (dr["code"] != null)
                {
                    ModuleRightModel mrModel = FillModuleRightModel(dr, allowAccessActionList, accessRightDA);

                    if (CommUtil.ConvertObjectToBool(dr["IsFatherNode"]) && deep <= 5)
                    {
                        mrModel.SubModule = GetSubModule(moduleDA, accessRightDA, allowAccessActionList, mrModel.ModuleCode, deep);
                    }
                    moduleRightList.Add(mrModel);
                }
            }
            return moduleRightList;
        }

        /// <summary>
        /// fill module accessright from datarow
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="allowAccessActionList"></param>
        /// <param name="accessRightDA"></param>
        /// <returns></returns>
        private ModuleRightModel FillModuleRightModel(DataRow dr, List<string> allowAccessActionList, AccessRightDA accessRightDA)
        {
            ModuleRightModel mrModel = new ModuleRightModel();
            mrModel.ModuleCode = CommUtil.ConvertObjectToString(dr["code"]);
            mrModel.ModuleName = CommUtil.ConvertObjectToString(dr["name"]);

            DataTable dtAccessRight = accessRightDA.GetAccessRightByModuleCode(dr["code"].ToString());
            Dictionary<string, bool> dicAction = new Dictionary<string, bool>();
            for (int j = 0; j < dtAccessRight.Rows.Count; j++)
            {
                DataRow drAction = dtAccessRight.Rows[j];
                if (drAction["name"] != null)
                {
                    if (allowAccessActionList.Contains(mrModel.ModuleCode + "_" + drAction["code"].ToString()))
                        dicAction[drAction["name"].ToString()] = true;
                    else
                        dicAction[drAction["name"].ToString()] = false;
                }
            }
            mrModel.DicAction = dicAction;
            return mrModel;
        }

        public  bool EditModuleAccessRight(string moduleActions, string groupID)
        {
            if (!string.IsNullOrEmpty(moduleActions))
            {
                AccessRightDA accessRightDA = null; 
                DataTable dtAction = new DataTable();
                try
                {
                    accessRightDA = new AccessRightDA();
                    dtAction = accessRightDA.GetAllAction();

                    Dictionary<string, string> dicAction = new Dictionary<string, string>();
                    for (int i = 0; i < dtAction.Rows.Count; i++)
                    {
                        DataRow dr = dtAction.Rows[i];
                        dicAction[CommUtil.ConvertObjectToString(dr["name"])] = CommUtil.ConvertObjectToString(dr["code"]);
                    }

                    accessRightDA.DeleteAccessRightByGroupID(groupID);

                    string[] rights = moduleActions.Split(',');
                    if (rights.Count() > 0)
                    {
                        foreach (string module_actions in rights)
                        {
                            if (!string.IsNullOrEmpty(module_actions))
                            {
                                string[] module_action = module_actions.Split('_');
                                if (module_action.Count() == 2)
                                {
                                    accessRightDA.InsertAccessRight(module_action[0], groupID, dicAction[module_action[1]], module_action[1], 1, UserID, DateTime.Now);
                                }

                            }
                        }
                    }
                }
                finally
                {
                    if (accessRightDA != null)
                        accessRightDA.CloseConnection();
                }
            }
            return true;
        }

    }
}
