using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Common
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 获取格式化时间
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetFormatDateTime(DateTime date, string format = "yyyy-MM-dd hh:mm:ss")
        {
            return date.ToString(format);
        }
    }
}
