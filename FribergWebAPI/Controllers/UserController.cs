using System.Threading.Tasks;
using FribergWebAPI.DTOs;
using FribergWebAPI.IdentityData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FribergWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}
		
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
		{
			var users = await _userManager.Users.ToListAsync();
			return Ok(users);
		}

		[HttpPost("register")]
		public async Task<IActionResult> RegisterRealtor(RegisterRealtorDto model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var realtor = new ApplicationUser
			{
				UserName = model.Email,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				PhoneNumber = model.Phone,
				Roles = model.Roles
			};

			// Hash the password
			var passwordHasher = new PasswordHasher<ApplicationUser>();
			realtor.PasswordHash = passwordHasher.HashPassword(realtor, model.Password);

			var result = await _userManager.CreateAsync(realtor, model.Password);

			if (result.Succeeded)
			{
				 foreach (var roleName in model.Roles)
				{
					var role = await _roleManager.FindByNameAsync(roleName);
					if (role != null)
					{
						await _userManager.AddToRoleAsync(realtor, role.Name);
					}
				}

				// Return success response
				return Ok(new { realtor.Id, realtor.Email });
			}
			else
			{
				// Return error response
				return BadRequest(result.Errors);
			}
		}
		
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(string id, RegisterRealtorDto model)
		{
			// Find the user by ID
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound(); // Return 404 if user not found
			}

			// Update user properties
			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.Email = model.Email;
			user.PhoneNumber = model.Phone;
			user.Roles = model.Roles;

			// Update the user
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return NoContent(); // Return 204 if user updated successfully
			}
			else
			{
				return BadRequest(result.Errors); // Return bad request with errors if update failed
			}
		}
		
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			// Delete the user
			var result = await _userManager.DeleteAsync(user);
			if (result.Succeeded)
			{
				return NoContent();
			}
			else
			{
				return BadRequest(result.Errors);
			}
		}
	}
}
