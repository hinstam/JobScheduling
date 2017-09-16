using JobScheduling.Entity.SecurityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JobScheduling.DataAccess.SecurityDA
{
    public class ModuleDA : Repository
    {
        private const string TEXT_GetMenuByUserID = "SELECT ModuleCode,ModuleController,ModuleAction,ModuleName,IsFatherNode FROM v_ccas_useraccessright where actionName=@actionName and fathercode is null and IsAllow=1 and useruid=@useruid and operation=@operation  and isshow = '1' order by moduleIndex";
        private const string TEXT_GetMenuByFatherCodeAndUserID = "SELECT ModuleCode,ModuleController,ModuleAction,ModuleName,IsFatherNode FROM v_ccas_useraccessright where actionName=@actionName and fathercode=@fathercode and IsAllow=1 and useruid=@useruid and operation=@operation  and isshow = '1' order by moduleIndex";
        private const string TEXT_GetAllLevel1Module = "SELECT * from t_ccas_module m where m.isdeleted = '0' and m.fathercode is null order by m.moduleIndex ";
        private const string TEXT_GetModuleByFatherCode = "SELECT * from t_ccas_module m where m.isdeleted = '0' and m.fathercode=@fathercode order by m.moduleIndex ";
        
        /// <summary>
        /// Get User Menu Information
        /// </summary>
        /// <param name="useruid"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public DataTable GetMenuByUserID(string useruid, string operation)
        {
            DataTable dt = Template.Query(TEXT_GetMenuByUserID, new string[] { "@actionName", "@useruid", "@operation" }, new string[] { ModuleAction.SHOW_NAME, useruid, operation });
            return dt;
        }

        /// <summary>
        /// Get User Menu Information by father code
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public DataTable GetMenuByCodeAndUserID(string fathercode, string useruid, string operation)
        {
            DataTable dt = Template.Query(TEXT_GetMenuByFatherCodeAndUserID, new string[] { "@actionName", "@fathercode", "@useruid", "@operation" }, new string[] { ModuleAction.SHOW_NAME, fathercode, useruid, operation });
            return dt;
        }

        public DataTable GetAllLevel1Module()
        {
            DataTable dt = Template.Query(TEXT_GetAllLevel1Module, null, null);
            return dt;
        }

        public DataTable GetModuleByFatherCode(string fatherCode)
        {
            DataTable dt = Template.Query(TEXT_GetModuleByFatherCode, new string[] { "@fathercode" }, new object[] { fatherCode });
            return dt;
        }
    }
}
