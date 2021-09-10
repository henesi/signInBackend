using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SignInAppSrv.Services;
using SignInAppSrv.Helpers;
using WebApp.Models;
using SignInAppSrv.Models;
using System.Threading.Tasks;
using System.Linq;

namespace SignInAppSrv.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentController : ControllerBase
    {
        private AssignmentService _assignmentService;
        private GroupService _groupService;
        private UserGroupService _userGroupService;
        private UserService _userService;
        private DayService _dayService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AssignmentController(
            AssignmentService assignmentService,
            GroupService groupService,
            UserGroupService userGroupService,
            DayService dayService,
            IMapper mapper,
            UserService userService,
            IOptions<AppSettings> appSettings)
        {
            _assignmentService = assignmentService;
            _groupService = groupService;
            _userGroupService = userGroupService;
            _dayService = dayService;
            _mapper = mapper;
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public IActionResult Create([FromBody]Assignment assignment)
        {
            if(TimeSpan.Compare(assignment.TimeTo, assignment.TimeFrom) != 1)
            {
                return StatusCode(400, new JsonException("Time end should be greater than start time", false));
            }
            _assignmentService.CreateAsync(assignment);
            return Ok(assignment);
        }

        [HttpPost("getbygroupid")]
        public IActionResult GetMyAssignmentByGroupId([FromBody] Assignment assignment)
        {
            AssignmentPerGroup assignmentPerGroup = new AssignmentPerGroup();
            assignmentPerGroup.monday = _assignmentService.GetAll().Where(x => x.GroupId == assignment.GroupId && x.DayId == 1);
            assignmentPerGroup.tuesday = _assignmentService.GetAll().Where(x => x.GroupId == assignment.GroupId && x.DayId == 2);
            assignmentPerGroup.wednesday = _assignmentService.GetAll().Where(x => x.GroupId == assignment.GroupId && x.DayId == 3);
            assignmentPerGroup.thursday = _assignmentService.GetAll().Where(x => x.GroupId == assignment.GroupId && x.DayId == 4);
            assignmentPerGroup.friday = _assignmentService.GetAll().Where(x => x.GroupId == assignment.GroupId && x.DayId == 5);
            assignmentPerGroup.saturday = _assignmentService.GetAll().Where(x => x.GroupId == assignment.GroupId && x.DayId == 6);
            assignmentPerGroup.sunday = _assignmentService.GetAll().Where(x => x.GroupId == assignment.GroupId && x.DayId == 7);
            return Ok(assignmentPerGroup);
        }

        [HttpPost("getspecificday")]
        public IActionResult GetMyAssignmentByGroupIdAndDay([FromBody] Assignment assignment)
        {
            AssignmentPerGroup assignmentPerGroup = new AssignmentPerGroup();
            var allAssignments = _assignmentService.GetAll();
            var query = allAssignments.Where(x => x.DayId == assignment.DayId && x.GroupId == assignment.GroupId).OrderBy(x => x.TimeFrom);
            return Ok(query);
        }

        [HttpGet("{id}")]
        public IActionResult GetByGroupId(int id)
        {
            return Ok(_assignmentService.GetByGroupId(id));

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Assignment assignment)
        {
            return Ok(_assignmentService.UpdateAsync(id, assignment));
            
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Assignment assignment)
        {
            _assignmentService.DeleteAsync(assignment.Id);
            return Ok();
        }
        [HttpPost("getnearest")]
        public IActionResult GetNearestAssignment([FromBody] Assignment assignment)
        {
            var timeToSearchFor = assignment.TimeFrom;
            var timeMidnight = TimeSpan.FromHours(0);
            var userID = _userService.GetUserID(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]);
            var allGroups = _groupService.GetAll();
            var allUserGroups = _userGroupService.GetByUserId(userID);
            var allAssignments = _assignmentService.GetAll().OrderBy(x => x.TimeFrom);
            var userGroups = from groups in allUserGroups
                             join assignments in allAssignments on groups.GroupId equals assignments.GroupId
                             where assignments.DayId == assignment.DayId
                             select new { assignments.Id, assignments.Name, assignments.Description, assignments.TimeFrom, assignments.TimeTo };
            var result = userGroups.FirstOrDefault(x => x.TimeFrom > assignment.TimeFrom);
            if (result == null)
            {
                result = userGroups.FirstOrDefault(x => x.TimeFrom > timeMidnight);
            }
            return Ok(result);
        }

    }
}