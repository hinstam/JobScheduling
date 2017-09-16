using JobScheduling.Common;
using JobScheduling.DataAccess.SecurityDA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace JobScheduling.Business.SecurityBL
{
    public class OperationBL:Business
    {
        public  List<SelectListItem> GetOperation()
        {
            List<SelectListItem> _operationList = new List<SelectListItem>();
            List<string> _operationCheck = new List<string>();

            OperationDA opDA = null; 
            DataTable dt = new DataTable();
            try
            {
                opDA = new OperationDA();
                dt = opDA.GetOperationByUserID(UserID);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    SelectListItem item = new SelectListItem();
                    item.Text = CommUtil.ConvertObjectToString(dr["Operation_desc"]);
                    item.Value = CommUtil.ConvertObjectToString(dr["Operation_code"]);
                    if (!_operationCheck.Contains(item.Text))
                    {
                        _operationCheck.Add(item.Text);
                        _operationList.Add(item);
                    }
                }

                if (_operationList.Count == 1)
                {
                    Operation = CommUtil.ConvertObjectToString(_operationList[0].Value);
                    OperationName = CommUtil.ConvertObjectToString(_operationList[0].Text);
                }
            }
            finally
            {
                if (opDA != null)
                    opDA.CloseConnection();
            }

            return _operationList;
        }

        public  List<SelectListItem> GetAllOperation()
        {
            List<SelectListItem> _operationList = new List<SelectListItem>();

            OperationDA opDA = null; 
            DataTable dt = new DataTable();
            try
            {
                opDA = new OperationDA();
                dt = opDA.GetAllOperation();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    SelectListItem item = new SelectListItem();
                    item.Text = CommUtil.ConvertObjectToString(dr["Operation_desc"]);
                    item.Value = CommUtil.ConvertObjectToString(dr["Operation_code"]);
                    _operationList.Add(item);
                }
            }
            finally
            {
                if (opDA != null)
                    opDA.CloseConnection();
            }

            return _operationList;
        }

        
    }
}
