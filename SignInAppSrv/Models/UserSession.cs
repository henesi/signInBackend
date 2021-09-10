using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignInAppSrv.Models
{
    public class UserSession
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public DateTime Start { get; set; }
        public string Token { get; set; }
    }
}
