using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CES.Api.Models;
using CES.Entities.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CES.Api.Controllers
{
    [Authorize(Roles = "User,Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private ILogoutCore _core;
        public LogoutController(ILogoutCore core)
        {
            _core = core;
        }

        // POST: api/Logout
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LogoutModel model)
        {
            if (ModelState.IsValid) 
            {
                var user  = await _core.Logout(model.Username,model.RefreshToken);

                if (string.IsNullOrEmpty(user?.Username)) 
                {
                    return BadRequest($"{model.Username} with refresh token {model.RefreshToken} does not exist");
                }

                return Ok(user);
            }
            return BadRequest();
        }

       
    }
}
