using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace SignInAppSrv.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public int creatorId { get; set; }
        public DateTime modifyTime { get; set; }
        public List<GroupMembership> groupMemberships { get; set; }
    }
}
