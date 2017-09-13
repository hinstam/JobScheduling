using JobScheduling.DataAccess.CommonDA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace JobScheduling.Business.CommonBL
{
    public class SelectionBL : Business
    {

        #region List<SelectListItem> 优化  jason 2013-11-20

        private List<SelectListItem> NewSelectList(DataTable table, object defaultValue, string TextFormat, params string[] colsName)
        {
            if (colsName == null || colsName.Count() == 0)
            {
                throw new ArgumentNullException("colsName is null！");
            }
            List<SelectListItem> result = new List<SelectListItem>();

            result.Add(new SelectListItem() { Value = string.Empty, Text = "-----------" });

            if (string.IsNullOrEmpty(TextFormat))
            {
                string defaultFormat = string.Empty;

                if (colsName.Count() == 1)
                {
                    defaultFormat = "{0}";
                }
                else
                {
                    for (int i = 1; i < colsName.Count(); i++)
                    {
                        defaultFormat += "{" + i + "}" + (i + 1 == colsName.Count() ? string.Empty : ",");
                    }
                }
                TextFormat = defaultFormat;
            }

            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow item in table.Rows)
                {
                    result.Add(new SelectListItem()
                    {
                        Value = item[0].ToString(),
                        Text = string.Format(TextFormat, item.ItemArray),
                        Selected = item[0].Equals(defaultValue),
                    });
                }
            }
            return result;
        }


        public virtual List<SelectListItem> GetSelectList(string tableName, object defaultValue, string TextFormat, string orderBy, bool isAll, params string[] colsName)
        {
            
            DataTable table=new DataTable ();
            SelectionDA selectionDA=null;
            try
            {
                selectionDA = new SelectionDA();

                table = selectionDA.GetDataTable(tableName, orderBy, isAll, colsName);
                selectionDA.CloseConnection();
            }            
            finally
            {
                if(selectionDA!=null)
                    selectionDA.CloseConnection();
            }
            
            
            
            return NewSelectList(table, defaultValue, TextFormat, colsName);
        }


        public virtual List<SelectListItem> GetSelectList(string tableName, object defaultValue, string TextFormat, bool isAll, params string[] colsName)
        {
            SelectionDA selectionDA =null;
            DataTable table = new DataTable();
            try
            {
                selectionDA = new SelectionDA();
                table = selectionDA.GetDataTable(tableName, null, isAll, colsName);
            }
            finally
            {
                if (selectionDA != null)
                    selectionDA.CloseConnection();
            }

            

            return NewSelectList(table, defaultValue, TextFormat, colsName);
        }


        public virtual List<SelectListItem> GetSelectList(string tableName, object defaultValue, bool isAll, params string[] colsName)
        {
            SelectionDA selectionDA =null;
            DataTable table = new DataTable();

            try
            {
                selectionDA = new SelectionDA();
                table = selectionDA.GetDataTable(tableName, null, isAll, colsName);
            }
            finally
            {
                if (selectionDA != null)
                    selectionDA.CloseConnection();
            }

            return NewSelectList(table, defaultValue, null, colsName);
        }


        public virtual List<SelectListItem> GetSelectList(DataTable table, object defaultValue, string TextFormat, params string[] colsName)
        {
            return NewSelectList(table, defaultValue, TextFormat, colsName);
        }


        public virtual List<SelectListItem> GetSelectList(string tableName, object defaultValue, string TextFormat, Dictionary<string, object> whereParam, params string[] colsName)
        {
            SelectionDA selectionDA =null;
            DataTable table = new DataTable();
            try
            {
                selectionDA = new SelectionDA();
                table = selectionDA.GetDataTable(tableName, whereParam, colsName);
            }
            finally
            {
                if (selectionDA != null)
                    selectionDA.CloseConnection();
            }

            return NewSelectList(table, defaultValue, TextFormat, colsName);
        }

        #endregion


    }
}
