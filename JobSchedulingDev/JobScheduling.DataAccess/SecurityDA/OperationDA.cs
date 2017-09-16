using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JobScheduling.DataAccess.SecurityDA
{
    public class OperationDA : Repository
    {
        private const string TEXT_GetOperationByUserID = "SELECT o.* FROM t_ccas_group g , operation o,t_ccas_user u, tr_ccas_user_group tr where tr.useruid=u.uid and tr.groupid=g.groupid and g.operation=o.Operation_code and u.userid=@userid and g.isdeleted=0 and u.isdeleted=0";
        private const string TEXT_GetAllOperation = "SELECT o.* FROM operation o ";
        //private const string TEXT_GetLocalCurrency = "Select * from tr_ccas_operation_currency where Operation_code =@operationCode ";
        private const string TEXT_GetLocalCurrency = "Select * from operation where Operation_code =@operationCode ";

        public DataTable GetOperationByUserID(string userid)
        {
            DataTable dt = Template.Query(TEXT_GetOperationByUserID, new string[] { "@userid"}, new string[] { userid });
            return dt;
        }

        public DataTable GetAllOperation()
        {
            DataTable dt = Template.Query(TEXT_GetAllOperation, null,null);
            return dt;
        }

        public DataTable GetLocalCurrency(string operationCode)
        {
            DataTable dt = Template.Query(TEXT_GetLocalCurrency, new string[] { "@operationCode" }, new string[] { operationCode });
            return dt;
        
        }

    }
}
