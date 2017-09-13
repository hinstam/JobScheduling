using JobScheduling.Entity.CommModel;
using JobScheduling.Model.FileModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JobScheduling.DataAccess.MasterDA
{
    public class ExportFileDataDA : Repository
    {

        public PagingModel GetBinFileByPage(string ExportDate, string Kind, PagingModel pm)
        {
            int totalCount = 0;

            StringBuilder SelectSQL = new StringBuilder("SELECT 1 as code, CONVERT(varchar(12) , ExportDate, 111 ) as 'Created Date', Path as 'File',Kind,CreatedBy ");
            SelectSQL.Append(" from t_ccas_export_master where 1=1 ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(ExportDate))
            {
                SelectSQL.Append(" and ExportDate = @ExportDate  ");
                pvs.Add("@ExportDate", "" + ExportDate + "");
            }
            if (!string.IsNullOrEmpty(Kind))
            {
                SelectSQL.Append(" and Kind like @Kind  ");
                pvs.Add("@Kind", "%" + Kind + "%");
            }

            SelectSQL.Append(" order by ExportDate desc");

            DataTable dt = Template.QueryByPage(SelectSQL.ToString(), pvs, pm.PageSize, pm.PageIndex, out totalCount);

            pm.DataTable = dt;
            pm.TotalCount = totalCount;

            return pm;
        }
    }
}
