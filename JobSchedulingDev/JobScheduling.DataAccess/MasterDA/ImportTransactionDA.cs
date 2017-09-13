using JobScheduling.Entity.CommModel;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.MasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.DataAccess.MasterDA
{
    public class ImportTransactionDA : Repository
    {
        public int CreateTempTable(string sql)
        {
            return Template.Execute(sql, null);

        }

        public int InsertTable(string sql, Dictionary<string, object> paramValues)
        {
            return Template.Execute(sql, paramValues);
        }

    }
}
