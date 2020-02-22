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
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private IApplicationCore _core;
        public ApplicationController(IApplicationCore core)
        {
            _core = core;
        }

        // POST: api/Application
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApplicationModel model)
        {
            if (ModelState.IsValid) 
            {
                var app = await _core.SaveAsync(model.ApplicationName);
                if (app.ID != Guid.Empty) 
                {
                    return Ok(app);
                }

            }
            return BadRequest();
        }      
    }
}
