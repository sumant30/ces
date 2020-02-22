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
    public class ApplicationRequestController : ControllerBase
    {
        private IUserCore _user;
        private IApplicationRequestCore _req;

        public ApplicationRequestController(IUserCore user,IApplicationRequestCore req)
        {
            _user = user;
            _req = req;
        }
        // POST: api/ApplicationRequest
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApplicationRequestModel model)
        {
            if (ModelState.IsValid)
            {
                Guid userId = await GetId(model.RefreshToken, model.Username);

                if (userId != Guid.Empty)
                {
                    if (await _req.RequestExists(userId, model.AppId)) 
                    {
                        return BadRequest("You have already requested access for this application.");
                    }
                    await _req.SaveAppRequest(userId, model.AppId, model.AccessType);
                    return NoContent();
                }
                return BadRequest($"{model.Username} with refresh token {model.RefreshToken} does not exist");
            }
            return BadRequest();
        }

        private async Task<Guid> GetId(string refreshToken, string username) => await _user.GetAsync(username, refreshToken);

    }
}
