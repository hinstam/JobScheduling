using JobScheduling.Entity.CommModel;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.MasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.DataAccess.MasterDA
{
    public class UpdateTransDA : Repository
    {
        public int UpdateTable(string sql)
        {
            return Template.Execute(sql,null);
        }
    }
}
