using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Entity.SecurityModel
{
    public class ModuleRightModel
    {
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public List<ModuleRightModel> SubModule { get; set; }
        public Dictionary<string, bool> DicAction { get; set; }
        
    }

    public class AccessRightModel
    {
        public List<GroupM> GroupList { get; set; }
        public List<ModuleRightModel> ModuleRightList { get; set; }
        public string SelectedGroupID { get; set; }
        public string SelectedGroupName { get; set; }
        public string ModuleActions { get; set; }
    }
}
