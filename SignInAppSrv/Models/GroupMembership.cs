using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace SignInAppSrv.Models
{
    public class GroupMembership
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [ForeignKey("GroupId")]
        public int GroupId { get; set; }
        [JsonIgnore]
        public Group Group { get; set; }
        public string userIdentifier { get; set; }
    }
}
