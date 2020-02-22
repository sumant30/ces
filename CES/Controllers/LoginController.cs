using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CES.Api.Helpers;
using CES.Api.Models;
using CES.Entities.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace CES.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginCore _core;       

        public LoginController(ILoginCore core)
        {
            _core = core;           
        }

        // POST: api/Login
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = await _core.AuthenticateAsync(model.Username, model.Password);
                if (!string.IsNullOrEmpty(dto?.Username))
                {
                    dto.GenerateToken();
                    return Ok(dto);
                }
                return NotFound($"{model.Username} does not exists for given password.");
            }
            return BadRequest();
        }

    }
}
