using JobScheduling.DataAccess.SecurityDA;
using JobScheduling.Model.CommModel;
using JobScheduling.Entity.SecurityModel;
using JobScheduling.DBCommon.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JobScheduling.Entity.CommModel;
using JobScheduling.Common;

namespace JobScheduling.Business.SecurityBL
{
    public class UserBL : Business
    {
        public  ResultModel Login(LoginViewModel model)
        {
            ResultModel result = new ResultModel();

            UserDA userDA = null; 
            DataTable dt = new DataTable();
            try
            {
                userDA = new UserDA();
                dt = userDA.GetUsers(model.UserID.Trim());

                if (dt == null)
                {
                    result.IsSuccess = false;
                    result.Exception = string.Format("User does not exist!");
                    return result;
                }

                DataRow dr = dt.Rows[0];

                if (!string.Equals(dr["password"], GetSHA512Encrypt(model.Password)))
                {
                    result.IsSuccess = false;
                    result.Exception = string.Format("The Password is incorrect");
                    return result;
                }

                if (!CommUtil.ConvertObjectToBool(dr["active"]))
                {
                    result.IsSuccess = false;
                    result.Exception = string.Format("User blocked, please contact your administrator!");
                    return result;
                }

                // TODO: Add web service logic here

                UserName = dr["UserName"].ToString();
                UserID = dr["UserID"].ToString();
                UserUID = dr["UID"].ToString();
                //Operation = "EEGHK";//在未完成Operation模块时作为测试数据
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            finally
            {
                if (userDA != null)
                    userDA.CloseConnection();
            }

            return result;
        }


        public  void Logout()
        {
            // TODO: Add web service logic here
            Session.RemoveAll();
        }


        public  ResultModel ChangePassword(string oldPassword, string password)
        {
            ResultModel result = new ResultModel();

            UserDA userDA = null; 
            DataRow dr ;
            try
            {
                userDA = new UserDA();
                dr = userDA.GetUsers(UserID).Rows[0];

                if (!string.Equals(dr["password"], GetSHA512Encrypt(oldPassword)))
                {
                    result.IsSuccess = false;
                    result.Exception = string.Format("The password is incorrect");
                    return result;
                }
                var val = new Dictionary<string, object>();

                val.Add("Password", GetSHA512Encrypt(password));

                result.Affected = userDA.EditUser(val, UserID);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            finally
            {
                if (userDA != null)
                    userDA.CloseConnection();
            }

            return result;
        }
        

        public  ResultModel ChangePasswordForAdmin(string userID, string password)
        {
            ResultModel result = new ResultModel();

            var val = new Dictionary<string, object>();

            val.Add("Password", GetSHA512Encrypt(password));

            UserDA userDA = null;
            try
            {
                userDA = new UserDA();

                result.Affected = userDA.EditUser(val, userID);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            finally
            {
                if (userDA != null)
                    userDA.CloseConnection();
            }

            return result;
        }


        private string GetSHA512Encrypt(string password)
        {
            string ciphertext = string.Empty;

            foreach (var item in System.Security.Cryptography.SHA512.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)))
            {
                ciphertext += item.ToString("X2");
            }

            return ciphertext;
        }


        public  ResultModel New(UserM model)
        {
            ResultModel result = new ResultModel();
            DateTime dt = DateTime.Now;

            UserDA userDA = null;

            try
            {
                userDA = new UserDA();

                if (userDA.GetUsers(model.UserID) != null)
                {
                    result.IsSuccess = false;
                    result.Exception = "User has been saved!";
                    return result;
                }

                Dictionary<string, object> paramValues = new Dictionary<string, object>();
                paramValues.Add("UserID", model.UserID.Trim());
                paramValues.Add("UserName", model.UserName.Trim());
                paramValues.Add("Password", GetSHA512Encrypt(model.Password));
                paramValues.Add("Active", model.Active);
                paramValues.Add("CreatedBy", UserID);
                paramValues.Add("CreatedTime", dt);
                paramValues.Add("LastModifiedBy", UserID);
                paramValues.Add("LastModifiedTime", dt);
                paramValues.Add("IsDeleted", 0);

                result.Affected = userDA.NewUser(paramValues);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            finally
            {
                if (userDA != null)
                    userDA.CloseConnection();
            }
            return result;
        }


        public  PagingModel List(string UserID, string UserName, int pageSize, int pageIndex)
        {
            
            PagingModel dt = new PagingModel();
            UserDA userDA = null; 
            try
            {
                userDA = new UserDA();
                dt = userDA.GetUserByPage(UserID, UserName, new PagingModel() { PageIndex = pageIndex, PageSize = pageSize });
            }
            finally
            {
                if (userDA != null)
                    userDA.CloseConnection();
            }

            return dt;
        }


        public  UserM GetUser(string userID)
        {
            UserDA userDA = null;
            DataRow dr;
            var u = new UserM { };
            try
            {
                userDA = new UserDA();

                dr = userDA.GetUsers(userID).Rows[0];

                u = new UserM
                {
                    UserID = dr["UserID"].ToString(),
                    UserName = dr["UserName"].ToString(),
                    Active = CommUtil.ConvertObjectToBool(dr["Active"]),
                    IsDeleted = Convert.ToByte(dr["IsDeleted"]),
                };
            }
            finally
            {
                if (userDA != null)
                    userDA.CloseConnection();
            }

            return u;
        }


        public  ResultModel Edit(UserM model)
        {
            ResultModel result = new ResultModel();

            UserDA userDA = null;
            try
            {
                userDA = new UserDA();

                Dictionary<string, object> saveData = new Dictionary<string, object>();

                saveData.Add("UserName", model.UserName.Trim());
                saveData.Add("Active", model.Active);
                saveData.Add("LastModifiedBy", UserID);
                saveData.Add("LastModifiedTime", DateTime.Now);

                if (model.IsDeleted == 1)
                {
                    saveData.Add("IsDeleted", model.IsDeleted);
                    saveData.Add("DeletedBy", UserID);
                    saveData.Add("DeletedTime", DateTime.Now);
                }

                if (!string.IsNullOrEmpty(model.Password))
                {
                    saveData.Add("Password", GetSHA512Encrypt(model.Password));
                }

                result.Affected = userDA.EditUser(saveData, model.UserID);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            finally
            {
                if (userDA != null)
                    userDA.CloseConnection();
            }

            return result;
        }


        public  ResultModel Delete(string userID)
        {
            ResultModel result = new ResultModel();

            UserDA userDA = null;
            try
            {
                userDA = new UserDA();

                Dictionary<string, object> saveData = new Dictionary<string, object>();

                saveData.Add("IsDeleted", 1);
                saveData.Add("DeletedBy", UserID);
                saveData.Add("DeletedTime", DateTime.Now);

                result.Affected = userDA.EditUser(saveData, userID);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
            }
            finally
            {
                if (userDA != null)
                    userDA.CloseConnection();
            }

            return result;
        }


        public  List<UserM> GetAllUser(string groupID, string userName)
        {
            List<UserM> result = new List<UserM>();

            UserDA userDA = null;
            try
            {
                userDA = new UserDA();

                var dt = userDA.GetUsersForName(groupID, userName);

                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        result.Add(new UserM() { UID = item["UID"].ToString(), UserName = item["UserName"].ToString() });
                    }
                }
            }
            finally
            {
                if (userDA != null)
                    userDA.CloseConnection();
            }

            return result;
        }


        public  List<UserM> GetGroupUser(string groupID, string userName)
        {
            List<UserM> result = new List<UserM>();

            UserDA userDA = null;
            try
            {
                userDA = new UserDA();

                var dt = userDA.GetUsersForGroup(groupID, userName);

                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        result.Add(new UserM() { UID = item["UID"].ToString(), UserName = item["UserName"].ToString() });
                    }
                }
            }
            finally
            {
                if (userDA != null)
                    userDA.CloseConnection();
            }

            return result;
        }

    }
}
