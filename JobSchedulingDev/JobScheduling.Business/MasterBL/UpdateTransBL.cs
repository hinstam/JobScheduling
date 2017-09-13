using JobScheduling.DataAccess.MasterDA;
using JobScheduling.Entity.CommModel;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.MasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Business.MasterBL
{
    public class UpdateTransBL : Business
    {
        public virtual UpdateTransM UpdateTrans()
        {
            UpdateTransM model = new UpdateTransM();
            UpdateTransDA importDA = null;
            try
            {
                importDA = new UpdateTransDA();
                string ls_sql = "update t_ccas_transaction_master set CardBrand=b.CardBrand,IssuingBank=b.IssuingBank,TypeofCard=b.TypeofCard,CategoryofCard=b.CategoryofCard,IssuingCountryCode=b.IssuingCountryISOA2Code from t_ccas_transaction_master a inner join t_ccas_bin_master b on a.bin=b.bin";
                int rel = importDA.UpdateTable(ls_sql);
                model.NumOfSuccess = rel;
                model.HasResult = true;
            }
            finally
            {
                if (importDA != null)
                    importDA.CloseConnection();
            }

            return model;
        }
       
    }
}
