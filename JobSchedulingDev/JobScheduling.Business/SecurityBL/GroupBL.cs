using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JobScheduling.DataAccess.SecurityDA;
using JobScheduling.Entity.CommModel;
using JobScheduling.Entity.SecurityModel;
using JobScheduling.Model.CommModel;

namespace JobScheduling.Business.SecurityBL
{
    public class GroupBL : Business
    {
        public PagingModel List(string groupID, string groupName, string description, int pageSize, int pageIndex)
        {
            GroupDA groupDA = null; 
            PagingModel dt = new PagingModel();

            try
            {
                groupDA = new GroupDA();
                dt = groupDA.GetGroupByPage(groupID, groupName, description, Operation, new PagingModel() { PageIndex = pageIndex, PageSize = pageSize });
            }
            finally
            {
                if (groupDA!=null) 
                    groupDA.CloseConnection();
            }

            return dt;
        }


        public ResultModel New(GroupM model)
        {
            ResultModel result = new ResultModel();
            DateTime dt = DateTime.Now;

            Dictionary<string, object> paramValues = new Dictionary<string, object>();
            paramValues.Add("GroupName", model.GroupName.Trim());
            paramValues.Add("Operation", Operation);
            paramValues.Add("SystemID", "S0001");
            paramValues.Add("Description", model.Description);
            paramValues.Add("CreatedBy", UserID);
            paramValues.Add("CreatedTime", dt);
            paramValues.Add("LastModifiedBy", UserID);
            paramValues.Add("LastModifiedTime", dt);
            paramValues.Add("NumOfUsers", 0);
            paramValues.Add("IsDeleted", 0);

            GroupDA groupDA = null;
            try
            {
                groupDA = new GroupDA();
                result.Affected = groupDA.NewGroup(paramValues);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            finally
            {
                if (groupDA != null)
                    groupDA.CloseConnection();
            }

            return result;
        }


        public ResultModel Edit(GroupM model)
        {
            ResultModel result = new ResultModel();

            Dictionary<string, object> saveData = new Dictionary<string, object>();

            saveData.Add("GroupName", model.GroupName.Trim());
            saveData.Add("Description", model.Description);
            saveData.Add("LastModifiedBy", UserID);
            saveData.Add("LastModifiedTime", DateTime.Now);

            if (model.IsDeleted == 1)
            {
                saveData.Add("IsDeleted", model.IsDeleted);
                saveData.Add("DeletedBy", UserID);
                saveData.Add("DeletedTime", DateTime.Now);
            }

            GroupDA groupDA = null;
            try
            {
                groupDA = new GroupDA();
                result.Affected = groupDA.EditGroup(saveData, model.GroupID);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            finally
            {
                if (groupDA != null)
                groupDA.CloseConnection();
            }
            return result;
        }


        public GroupM GetGroup(string groupID)
        {
            GroupDA groupDA = null;
            DataRow dr ;
            var g = new GroupM {};
            try
            {
                groupDA = new GroupDA();
                dr = groupDA.GetGroups(groupID, Operation).Rows[0];

                 g = new GroupM
                {
                    GroupID = dr["GroupID"].ToString(),
                    GroupName = dr["GroupName"].ToString(),
                    Description = dr["Description"].ToString(),
                    IsDeleted = Convert.ToByte(dr["IsDeleted"]),
                };
            }
            finally
            {
                if (groupDA != null)
                    groupDA.CloseConnection();
            }
            return g;
        }


        public ResultModel Delete(string groupID)
        {
            ResultModel result = new ResultModel();

            Dictionary<string, object> saveData = new Dictionary<string, object>();

            saveData.Add("IsDeleted", 1);
            saveData.Add("DeletedBy", UserID);
            saveData.Add("DeletedTime", DateTime.Now);

            GroupDA groupDA = null;
            try
            {
                groupDA = new GroupDA();
                result.Affected = groupDA.EditGroup(saveData, groupID);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            finally
            {
                if (groupDA != null)
                    groupDA.CloseConnection();
            }

            return result;
        }


        public ResultModel AddUserGroup(string groupID, string UserUIDs)
        {
            ResultModel result = new ResultModel();
            Dictionary<string, object> saveData;
            GroupDA groupDA = null;
            try
            {
                groupDA = new GroupDA();
                //这里有待优化。
                foreach (var item in UserUIDs.Split(',').Where(z => z.Length > 0))
                {
                    saveData = new Dictionary<string, object>();
                    saveData.Add("GroupID", groupID);
                    saveData.Add("UserUID", item);
                    result.Affected = groupDA.NewUserGroup(saveData);
                }

                var UserGroups = groupDA.GetUserGroups(groupID);

                int count = UserGroups == null ? 0 : UserGroups.Rows.Count;

                saveData = new Dictionary<string, object>();

                saveData.Add("NumOfUsers", count);

                groupDA.EditGroup(saveData, groupID);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            finally
            {
                if (groupDA != null)
                    groupDA.CloseConnection();
            }
            return result;
        }


        public ResultModel DelUserGroup(string groupID, string UserUIDs)
        {
            ResultModel result = new ResultModel();
            Dictionary<string, object> DelData;
            GroupDA groupDA = null;
            try
            {
                groupDA = new GroupDA();
                foreach (var item in UserUIDs.Split(',').Where(z => z.Length > 0))
                {
                    DelData = new Dictionary<string, object>();
                    DelData.Add("GroupID", groupID);
                    DelData.Add("UserUID", item);
                    result.Affected = groupDA.DelUserGroup(DelData);
                }

                var UserGroups = groupDA.GetUserGroups(groupID);

                int count = UserGroups == null ? 0 : UserGroups.Rows.Count;

                DelData = new Dictionary<string, object>();

                DelData.Add("NumOfUsers", count);

                groupDA.EditGroup(DelData, groupID);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            finally
            {
                if (groupDA != null)
                    groupDA.CloseConnection();
            }

            return result;
        }


        public List<GroupM> GetAllGroup(string GroupName)
        {
            List<GroupM> result = new List<GroupM>();
            GroupDA groupDA = null;
            try
            {
                groupDA = new GroupDA();
                var dt = groupDA.GetGroupsForName(GroupName, Operation);

                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        result.Add(new GroupM() { GroupID = item["GroupID"].ToString(), GroupName = item["GroupName"].ToString() });
                    }
                }
            }
            finally
            {
                if (groupDA != null)
                    groupDA.CloseConnection();
            }

            return result;
        }




    }
}
