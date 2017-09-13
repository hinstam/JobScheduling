using JobScheduling.DataAccess.MasterDA;
using JobScheduling.Entity.CommModel;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.FileModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Business.MasterBL
{
    public class ExportFileDataBL : Business
    {
        public virtual PagingModel List(string ExportDate, string Kind, int pageSize, int pageIndex)
        {
            ExportFileDataDA binFileDA = null;
            PagingModel dt = new PagingModel();
            try
            {
                binFileDA = new ExportFileDataDA();
                dt = binFileDA.GetBinFileByPage(ExportDate, Kind, new PagingModel() { PageIndex = pageIndex, PageSize = pageSize });
            }
            finally
            {
                if (binFileDA != null)
                    binFileDA.CloseConnection();
            }
            return dt;
        }
    }
}
