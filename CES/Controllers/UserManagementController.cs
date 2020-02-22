using CES.Api.Models;
using CES.Entities.Enums;
using CES.Entities.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace CES.Api.Controllers
{
	[Authorize(Roles = "Administrator")]
	[Route("api/[controller]")]
	[ApiController]
	public class UserManagementController : ControllerBase
	{
		private IUserCore _user;
		private IUserManagementCore _mgmt;
		public UserManagementController(IUserCore user, IUserManagementCore mgmt)
		{
			_user = user;
			_mgmt = mgmt;
		}
		// GET: api/UserManagement
		[HttpGet]
		public async Task<IActionResult> Get(string refreshToken,string grantType)
		{
			if (!string.IsNullOrEmpty(refreshToken) || !string.IsNullOrEmpty(grantType))
			{
				if (grantType?.ToLower() != GrantTypes.Sent.ToString().ToLower())
				{
					return BadRequest("Grant type of sent is required.");
				}

				Claim username = GetEmailAddress();
				Guid adminId = await GetAdminId(refreshToken, username);

				if (adminId != Guid.Empty)
				{
					var users = await _mgmt.Get();
					return Ok(users);
				}
				else
				{
					return BadRequest($"{username} with refresh token {refreshToken} does not exist");
				}
			}
			return BadRequest();
		}
		
		// GET: api/UserManagement/{userId}
		[HttpGet("{id:Guid}", Name = "Get")]
		public async Task<IActionResult> Get(Guid id, string refreshToken, string grantType)
		{
			if (!string.IsNullOrEmpty(refreshToken) || !string.IsNullOrEmpty(grantType))
			{
				if (grantType?.ToLower() != GrantTypes.Sent.ToString().ToLower())
				{
					return BadRequest("Grant type of sent is required.");
				}

				Claim username = GetEmailAddress();
				Guid adminId = await GetAdminId(refreshToken, username);

				if (adminId != Guid.Empty)
				{
					var user = await _mgmt.Get(id);

					return Ok(user);
				}
				return BadRequest($"{username} with refresh token {refreshToken} does not exist");
			}
			return BadRequest();
		}
				

		

		private async Task<Guid> GetAdminId(string refreshToken, Claim username) => await _user.GetAsync(username?.Value, refreshToken);

		private Claim GetEmailAddress() => @User.Claims.FirstOrDefault(c => c.Type.Contains("emailaddress"));
	}
}
