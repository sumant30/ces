using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CES.Entities.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CES.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserCore _core;
        public UserController(IUserCore core)
        {
            _core = core;
        }
        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Claim username = GetEmailAddress();
            var details = await _core.GetUserSessionDetails(username?.Value);
            return Ok(details);
           
        }

        // PUT
        [Route("ChangePassword/{newPassword}")]
        [HttpPut]
        public async Task<IActionResult> Put(string newPassword)
        {
            Claim username = GetEmailAddress();
            await _core.ChangePassword(username?.Value, newPassword);
            return Ok("Password changed successfully.");
        }

        // GET: api/User/ForgotPassword
        [Route("ForgotPassword")]
        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            Claim username = GetEmailAddress();
            return Ok(await _core.ForgotPassword(username?.Value));
        }

        private Claim GetEmailAddress() => @User.Claims.FirstOrDefault(c => c.Type.Contains("emailaddress"));
    }
}
