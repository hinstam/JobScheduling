using JobScheduling.Business.SecurityBL;
using JobScheduling.Entity.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobScheduling.Web.Extensions
{
    public class ModuleAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContextBase contextBase = filterContext.RequestContext.HttpContext;

            //check user login or not
            if (contextBase.Session["UserID"] == null || contextBase.Session["UserName"] == null)
                TransferToPermissionDeniedPage(filterContext);
            else
            {
                if (contextBase.Request.RequestType.Equals("GET"))
                {
                    string controllerName = filterContext.RouteData.Values["controller"].ToString().ToLower();
                    string actionName = filterContext.RouteData.Values["action"].ToString().ToLower();

                    if (actionName.StartsWith("noauth_"))
                        return;

                    //check user accessright
                    string moduleBelong = null;
                    Dictionary<string, bool> ar = GetUserAccessRight(contextBase, controllerName, out moduleBelong);
                    if (CheckUserAllowAccess(ar, actionName))
                    {
                        //set user accessright to ViewData;
                        filterContext.Controller.ViewData.Clear();
                        foreach (KeyValuePair<string, bool> pair in ar)
                        {
                            filterContext.Controller.ViewData.Add(pair.Key, pair.Value);
                        }
                        SetUserMenuSession(contextBase, moduleBelong);
                    }
                    else
                    {
                        TransferToPermissionDeniedPage(filterContext);
                    }
                }
            }
        }

        private bool CheckUserAllowAccess(Dictionary<string, bool> ar,string actionName)
        {
            if (!ar.ContainsKey(ModuleAction.SHOW_NAME.ToLower()) || ar[ModuleAction.SHOW_NAME.ToLower()] == false)
                return false;

            if (!actionName.Equals(ModuleAction.LIST_NAME.ToLower()) && !actionName.Equals(ModuleAction.INDEX_NAME.ToLower()))
            {
                if (!ar.ContainsKey(actionName) || ar[actionName] == false)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Transfer to permission denied page
        /// </summary>
        /// <param name="response"></param>
        private void TransferToPermissionDeniedPage(ActionExecutingContext filterContext)
        {
            //因为List页面中采用部分视图，如果使用Redirect，那么登录页面就会返回到列表中。
            //contextBase.Response.Redirect("~/Home/Login");
            //contextBase.Response.Write("<script>window.location.href='/home/Sorry';</script>");

            ContentResult result = new ContentResult();
            result.Content = "<script>window.location.href='/home/Sorry';</script>";
            filterContext.Result = result;

            filterContext.RequestContext.HttpContext.Session.RemoveAll();           
        }

        /// <summary>
        /// check user accessright
        /// </summary>
        /// <param name="contextBase"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        private Dictionary<string, bool> GetUserAccessRight(HttpContextBase contextBase, string controllerName, out string moduleBelong)
        {
            if (contextBase.Session[controllerName] == null)
            {
                AccessRightBL accessRightBL = new AccessRightBL();
                Dictionary<string, bool> dicAR = accessRightBL.GetAccessRightByControllerName(controllerName, out moduleBelong);
                contextBase.Session[controllerName] = dicAR;
                contextBase.Session[controllerName + "_Module"] = moduleBelong;
                return dicAR;
            }
            else
            {
                if (contextBase.Session[controllerName + "_Module"] != null)
                    moduleBelong = contextBase.Session[controllerName + "_Module"].ToString();
                else
                    moduleBelong = null;

                return (Dictionary<string, bool>)contextBase.Session[controllerName];
            }
        }

        private void SetUserMenuSession(HttpContextBase contextBase, string controllerName)
        {
            //set Menu session
            contextBase.Session["MenuController"] = controllerName;
            //contextBase.Session["MenuAction"] = actionName;
        }
       
    }
}