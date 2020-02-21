using CES.Api.Helpers;
using CES.Api.Models;
using CES.Entities.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CES.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IUserCore _user;
        private ITokenCore _core;

        public TokenController(IUserCore user, ITokenCore core)
        {
            _user = user;
            _core = core;
        }

        // POST: api/Token/Refresh
        [Route("Refresh")]
        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenModel model)
        {
            //Validate client secret for username
            if (ModelState.IsValid)
            {
                var isValid = await _user.ValidateClientSecret(model.Username, model.ClientSecret);
                if (isValid)
                {
                    Guid userId = await GetUserId(model.RefreshToken, model.Username);

                    if (userId != Guid.Empty)
                    {
                        var dto = await _core.GenerateNewTokenAsync(userId);
                        dto.GenerateToken();
                        return Ok(dto);
                    }
                    else
                    {
                        return BadRequest($"User with refresh token {model.RefreshToken} is not present.");
                    }
                }
                return BadRequest($"The {model.ClientSecret} is not valid for {model.Username}");
            }
            return BadRequest();
        }

        // POST: api/Token/Revoke
        [Authorize(Roles = "Administrator")]
        [Route("Revoke")]
        [HttpPost]
        public async Task<IActionResult> Revoke([FromBody]RevokeTokenModel model)
        {
            if (ModelState.IsValid)
            {
                await _core.Revoke(model.UserId);
                return NoContent();
            }
            return BadRequest();
        }

        private async Task<Guid> GetUserId(string refreshToken, string username) => await _user.GetAsync(username, refreshToken);
    }
}
