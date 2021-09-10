using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignInAppSrv.Helpers
{
    public class JsonException
    {
        public string message { get; set; }
        public bool forceLogout { get; set; }

        public JsonException(string msg, bool forceLogout)
        {
            this.message = msg;
            this.forceLogout = forceLogout;
        }
    }
}
