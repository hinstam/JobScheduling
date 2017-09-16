using JobScheduling.Entity.CommModel;
using JobScheduling.Model.FileModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JobScheduling.DataAccess.FileDA
{
    public class BinFileDA:Repository
    {
        private const string TEXT_GetBinFileByCode = "select * from t_ccas_bin_master where  bin=@Code";

        private const string TEXT_InsertBinFileByCode = @"insert into t_ccas_bin_master(ManualAuto,BIN,CardBrand,IssuingBank,
            TypeofCard,CategoryofCard,IssuingCountryISOA2Code,CreatedDate,LastUpdateDate,UpdatedBy ) values 
            (@ManualAuto,@BIN,@CardBrand,@IssuingBank,@TypeofCard,@CategoryofCard,@IssuingCountryISOA2Code,@CreatedDate,@LastUpdateDate,@UpdatedBy)";

        private const string TEXT_UpdateBinFileByCode = @"update t_ccas_bin_master set ManualAuto=@ManualAuto,CardBrand=@CardBrand,
            IssuingBank=@IssuingBank,TypeofCard=@TypeofCard,CategoryofCard=@CategoryofCard,IssuingCountryISOA2Code=@IssuingCountryISOA2Code,
            LastUpdateDate=@LastUpdateDate,UpdatedBy= @UpdatedBy where BIN=@BIN ";

        private const string TEXT_DelBinFileByCode = "delete from t_ccas_bin_master where  bin=@Code";

        public DataRow GetBinFileByCode(string code)
        {
            DataTable dt = Template.Query(TEXT_GetBinFileByCode, new string[] { "@Code" }, new object[] { code });
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int NewBinFile(object[] paramsValue)
        {
            return Template.Execute(TEXT_InsertBinFileByCode, new string[] { "@ManualAuto", "@BIN", "@CardBrand", "@IssuingBank", "@TypeofCard",
                "@CategoryofCard","@IssuingCountryISOA2Code","@CreatedDate","@LastUpdateDate","@UpdatedBy" }, paramsValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramsValue"></param>
        /// <returns></returns>
        public int UpdateBinFile(object[] paramsValue)
        {
            return Template.Execute(TEXT_UpdateBinFileByCode, new string[] { "@ManualAuto", "@BIN", "@CardBrand", "@IssuingBank", "@TypeofCard",
                "@CategoryofCard","@IssuingCountryISOA2Code","@LastUpdateDate","@UpdatedBy" }, paramsValue);
        }

        public int Delete(object[] paramsValue)
        {
            return Template.Execute(TEXT_DelBinFileByCode, new string[] { "@Code"}, paramsValue);
        }

        public PagingModel GetBinFileByPage(string code, PagingModel pm)
        {
            int totalCount = 0;

            StringBuilder SelectSQL = new StringBuilder("SELECT BIN as 'code', BIN,CardBrand,IssuingBank,TypeofCard,CategoryofCard,IssuingCountryISOA2Code ");
            SelectSQL.Append(" from t_ccas_bin_master where 1=1 ");

            Dictionary<string, object> pvs = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(code))
            {
                SelectSQL.Append(" and BIN like @BIN  ESCAPE '/' ");
                pvs.Add("@BIN", "%" + code + "%");
            }

            SelectSQL.Append(" order by LastUpdateDate desc");

            DataTable dt = Template.QueryByPage(SelectSQL.ToString(), pvs, pm.PageSize, pm.PageIndex, out totalCount);

            pm.DataTable = dt;
            pm.TotalCount = totalCount;

            return pm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public BinFileM Row2Object(DataRow row)
        {
            BinFileM model = null;
            if (row != null)
            {
                model = new BinFileM();
                model.BIN = (string)row["BIN"];
                model.ManualAuto = row["ManualAuto"] == DBNull.Value ? null : (string)row["ManualAuto"];
                model.CardBrand = row["CardBrand"] == DBNull.Value ? null : (string)row["CardBrand"]; ;
                model.CategoryofCard = row["CategoryofCard"] == DBNull.Value ? null : (string)row["CategoryofCard"]; ;
                model.IssuingBank = row["IssuingBank"] == DBNull.Value ? null : (string)row["IssuingBank"];
                model.IssuingCountryISOA2Code = row["IssuingCountryISOA2Code"] == DBNull.Value ? null : (string)row["IssuingCountryISOA2Code"];
                model.TypeofCard = row["TypeofCard"] == DBNull.Value ? null : (string)row["TypeofCard"];

            }
            return model;
        }
    }
}
