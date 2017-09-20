using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Model.GanttSourceModel
{
    public class GanttValueModel
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Label { get; set; }

        public string Desc { get; set; }

        public string CustomClass { get; set; }

        public object DataObj { get; set; }
    }
}
