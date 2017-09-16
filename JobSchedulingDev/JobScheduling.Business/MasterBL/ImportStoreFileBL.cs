using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobScheduling.DataAccess.MasterDA;
using JobScheduling.Model.MasterModel;

namespace JobScheduling.Business.MasterBL
{
    public class ImportStoreFileBL:Business
    {
        public virtual ImportStoreFileM SaveFileToDB(string filenpath, string ls_exportpath, string webPath)
        {
            ImportStoreFileDA importDA =new ImportStoreFileDA();
            return importDA.SaveFileToDB(filenpath, ls_exportpath, webPath,UserID);
        }

        public void DeleteData()
        {
            ImportStoreFileDA importDA = null;
            try
            {
                importDA = new ImportStoreFileDA();
                string ls_sql = "truncate table t_ccas_store_master";
                int re = importDA.InsertTable(ls_sql, null);
            }
            finally
            {
                if (importDA != null)
                    importDA.CloseConnection();
            }
        }
    }
}
