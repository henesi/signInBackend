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
    public class DayController : ControllerBase
    {
        private IRepository<Day> _dayService;

        public DayController(DayService dayService)
        {
            _dayService = dayService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var day = _dayService.GetByGroupId(id);
            return Ok(day.Result);
        }

    }
}