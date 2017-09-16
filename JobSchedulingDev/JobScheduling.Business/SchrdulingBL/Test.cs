using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobScheduling.Business.TestService;

namespace JobScheduling.Business.SchrdulingBL
{
    public class Test
    {
        public string GetUser()
        {
            using(userServiceClient client = new userServiceClient())
            {
                return client.getInfo("panlin");
            }
        
        }

       
    }
}
