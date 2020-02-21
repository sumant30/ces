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
   
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private ITokenRepo _repo;

        public TokenController(ITokenRepo repo)
        {
            _repo = repo;
        }    
        
        // POST: api/Token/Refresh
        [Authorize(Roles = "User,Administrator")]
        [Route("Refresh")]
        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // POST: api/Token/Revoke
        [Authorize(Roles = "Administrator")]
        [Route("Revoke")]
        [HttpPost]
        public async Task<IActionResult> Revoke([FromBody] string value)
        {
            throw new NotImplementedException();
        }       
    }
}
