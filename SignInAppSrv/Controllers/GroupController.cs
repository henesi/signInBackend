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
using SignInAppSrv.dbContext;

namespace SignInAppSrv.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private GroupService _groupService;
        private UserGroupService _userGroupService;
        private UserService _userService;
        private DataContext _db;
        private readonly AppSettings _appSettings;

        public GroupController(
            GroupService groupService,
            UserGroupService userGroupService,
            DataContext db,
            UserService userService,
            IOptions<AppSettings> appSettings)
        {
            _groupService = groupService;
            _userGroupService = userGroupService;
            _db = db;
            _userService = userService;
            _appSettings = appSettings.Value;
        }
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var group = _groupService.GetById(id);
            var membership = _userGroupService.GetByGroupId(group.Id).ToList();
            return Ok(new Group
            {
                Id = group.Id,
                Identifier = group.Identifier,
                Name = group.Name,
                groupMemberships = membership
            });
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userID = _userService.GetUserID(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]);
            var allGroups = _groupService.GetAll();
            var allUserGroups = _userGroupService.GetByUserId(userID);
            var query = from groups in allGroups
                        join usergroups in allUserGroups on groups.Id equals usergroups.GroupId
                        select new { Id=groups.Id, Name =groups.Name, Identifier = groups.Identifier, ModifyTime = groups.modifyTime, groupMemberships = groups.groupMemberships};
            return Ok(query);
        }

        [HttpPost("search")]
        public IActionResult GetByName([FromBody]Group groupDto)
        {
            var userID = _userService.GetUserID(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]);
            var allGroups = _groupService.GetAll();
            var allUserGroups = _userGroupService.GetByUserId(userID);
            var query = from groups in allGroups
                        join usergroups in allUserGroups on groups.Id equals usergroups.GroupId
                        select new { Name =groups.Name, Identifier = groups.Identifier, ModifyTime = groups.modifyTime, groupMemberships = groups.groupMemberships};
            var queryByName = query.Where(x => x.Name.Contains(groupDto.Name));
            return Ok(queryByName);
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult Create([FromBody]Group groupDto)
        {
            var userID = _userService.GetUserID(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]);
            var userIdentifier = _userService.GetUserIdentifier(userID);

            groupDto.creatorId = userID;
            groupDto.modifyTime = DateTime.Now;

            var group = _groupService.CreateAsync(groupDto);

            if(group == null) { return StatusCode(400, new JsonException("Unable to create group", false)); }

            var usergroup = _userGroupService.CreateAsync(new GroupMembership
            {
                GroupId = group.Id,
                UserId = userID,
                userIdentifier = userIdentifier
            });
            // return basic user info (without password) and token to store client side
            return Ok(groupDto);
        }

        [HttpPost("join")]
        [Produces("application/json")]
        public IActionResult JoinTo([FromBody] Group gr)
        {
            var userID = _userService.GetUserID(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]);
            var userIdentifier = _userService.GetUserIdentifier(userID);

            var group = _groupService.GetByIdentifier(gr.Identifier);
            if (group == null) { return StatusCode(400, new JsonException("Group does not exist", false)); }

            var groupMembership = _userGroupService.GetByUserId(userID);

            var userGroup = new GroupMembership
            {
                UserId = userID,
                GroupId = group.Result.Id,
                userIdentifier = userIdentifier
            };

            userGroup = _userGroupService.CreateAsync(userGroup);
            if (userGroup == null) { return StatusCode(400, new JsonException("Already joined to this group", false)); }
            return Ok(userGroup);
        }

       
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

    }
}