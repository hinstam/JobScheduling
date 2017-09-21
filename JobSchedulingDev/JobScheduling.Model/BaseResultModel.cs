using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Model
{
    public class BaseResultModel
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
