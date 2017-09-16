using JobScheduling.DataAccess.SecurityDA;
using JobScheduling.Entity.SecurityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JobScheduling.Common;

namespace JobScheduling.Business.SecurityBL
{
    public class ModuleBL : Business
    {
        public  IEnumerable<MenuModel> GetMenu()
        {
            List<string> moduleList = new List<string>();

            List<MenuModel> menuList = new List<MenuModel>();
            ModuleDA moduleDA = null; 
            DataTable dt = new DataTable();
            try
            {
                moduleDA = new ModuleDA();
                dt = moduleDA.GetMenuByUserID(UserUID, Operation);

                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    if ("".Equals(CommUtil.ConvertObjectToString(dr["ModuleCode"])) || moduleList.Contains(CommUtil.ConvertObjectToString(dr["ModuleCode"])))
                    {
                        continue;
                    }
                    else
                    {
                        moduleList.Add(CommUtil.ConvertObjectToString(dr["ModuleCode"]));
                    }

                    MenuModel mm = new MenuModel();
                    mm.ModuleCode = CommUtil.ConvertObjectToString(dr["ModuleCode"]);
                    mm.ModuleName = CommUtil.ConvertObjectToString(dr["ModuleName"]);
                    mm.ModuleController = CommUtil.ConvertObjectToString(dr["ModuleController"]);
                    mm.ModuleAction = CommUtil.ConvertObjectToString(dr["ModuleAction"]);
                    mm.IsFatherNode = CommUtil.ConvertObjectToBool(dr["IsFatherNode"]);
                    bool isSelect = false;
                    if (mm.IsFatherNode)
                        mm.SubMenu = GetSubMenu(mm.ModuleCode, 1, out isSelect);
                    else
                        isSelect = CheckMenuSelected(mm.ModuleController, mm.ModuleAction);

                    mm.IsSelect = isSelect;
                    menuList.Add(mm);
                }
            }
            finally
            {
                if (moduleDA != null)
                    moduleDA.CloseConnection();
            }

            return menuList;
        }

        private List<MenuModel> GetSubMenu(string code,int deep,out bool isSelect)
        {
            bool temp = false;
            isSelect = false;
            List<string> moduleList = new List<string>();
            List<MenuModel> menuList = new List<MenuModel>();
            ModuleDA moduleDA = null; 
            DataTable dt = new DataTable();

            try
            {
                moduleDA = new ModuleDA();
                dt = moduleDA.GetMenuByCodeAndUserID(code, UserUID, Operation);


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    if ("".Equals(CommUtil.ConvertObjectToString(dr["ModuleCode"])) || moduleList.Contains(CommUtil.ConvertObjectToString(dr["ModuleCode"])))
                    {
                        continue;
                    }
                    else
                    {
                        moduleList.Add(CommUtil.ConvertObjectToString(dr["ModuleCode"]));
                    }

                    MenuModel mm = new MenuModel();
                    mm.ModuleCode = CommUtil.ConvertObjectToString(dr["ModuleCode"]);
                    mm.ModuleName = CommUtil.ConvertObjectToString(dr["ModuleName"]);
                    mm.ModuleController = CommUtil.ConvertObjectToString(dr["ModuleController"]);
                    mm.ModuleAction = CommUtil.ConvertObjectToString(dr["ModuleAction"]);
                    mm.IsFatherNode = CommUtil.ConvertObjectToBool(dr["IsFatherNode"]);

                    if (mm.IsFatherNode && deep <= 5)
                    {
                        mm.SubMenu = GetSubMenu(mm.ModuleCode, deep + 1, out isSelect);
                    }
                    else
                    {
                        isSelect = CheckMenuSelected(mm.ModuleController, mm.ModuleAction);
                    }

                    mm.IsSelect = isSelect;

                    //subMenu is selected
                    if (isSelect)
                        temp = true;

                    menuList.Add(mm);
                }

                //if submenu is selectd father menu is selected
                if (temp)
                    isSelect = true;
            }
            finally
            {
                if (moduleDA != null)
                    moduleDA.CloseConnection();
            }

            return menuList;
        }

        /// <summary>
        /// //Get session to check menu is selected or not
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private bool CheckMenuSelected(string controller,string action)
        {
            if (controller != null && action != null && controller.ToLower().Equals(Session["MenuController"] == null ? null : Session["MenuController"].ToString().ToLower()) /**&& action.ToLower().Equals(Session["MenuAction"])**/)
                return true;
            else
                return false;

        }

    }
}
