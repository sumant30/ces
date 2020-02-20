﻿using System;
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
        private ILogger<LoginController> _logger;
        private IConfiguration _config;

        public LoginController(ILoginCore core, ILogger<LoginController> logger, IConfiguration config)
        {
            _core = core;
            _logger = logger;
            _config = config;
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
                return NotFound($"{model.Username} does not exists");
            }
            return BadRequest("Username & Password should not be empty.");
        }

    }
}