using EG.CCAS.Entity.CommModel;
using EG.CCAS.Model.CommModel;
using EG.CCAS.Model.MasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EG.CCAS.DataAccess.MasterDA
{
    public class InportTransationDA : Repository
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
