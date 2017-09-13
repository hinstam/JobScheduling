using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Entity.SecurityModel
{
    public class MenuModel
    {
        public string ModuleController { get; set; }
        public string ModuleAction { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public bool IsFatherNode { get; set; }
        public List<MenuModel> SubMenu { get; set; }
        public bool IsSelect { get; set; }
    }
}
