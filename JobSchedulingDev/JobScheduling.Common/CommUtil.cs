using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Common
{
    public class CommUtil
    {
        public static string ConvertObjectToString(Object obj)
        {
            if (obj != null)
                return obj.ToString();
            else
                return "";
        }


        public static bool ConvertObjectToBool(Object obj)
        {
            if (obj != null)
            {
                bool result = false;
                Boolean.TryParse(obj.ToString(), out result);

                if (result)
                    return Convert.ToBoolean(obj);
                else
                {
                    if ("1".Equals(obj.ToString()) || "true".Equals(obj.ToString().ToLower()))
                        return true;
                    else 
                        return false;
                }
            }
            else
                return false;
        }
    }
}
