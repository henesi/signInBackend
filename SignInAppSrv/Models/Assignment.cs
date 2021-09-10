using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Models;

namespace SignInAppSrv.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        [Required]
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan TimeFrom { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan TimeTo { get; set; }
        public Day Day { get; set; }
        public int DayId { get; set; }
    }

    public class AssignmentPerGroup
    {
        public IEnumerable<Assignment> monday { get; set; }
        public IEnumerable<Assignment> tuesday { get; set; }
        public IEnumerable<Assignment> wednesday { get; set; }
        public IEnumerable<Assignment> thursday { get; set; }
        public IEnumerable<Assignment> friday { get; set; }
        public IEnumerable<Assignment> saturday { get; set; }
        public IEnumerable<Assignment> sunday { get; set; }
    }
}
