using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Model.GanttSourceModel
{
    public class GanttSourceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public string CssClass { get; set; }

        public IList<GanttValueModel> Values { get; set; }
    }
}
