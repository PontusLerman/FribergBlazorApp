using System.Threading.Tasks;
using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.Data.Repositories;
using FribergWebAPI.DTOs;
using FribergWebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FribergWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RealtorController : ControllerBase
	{
		private readonly UserManager<Realtor> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IAgency _agency;
		
		public RealtorController(UserManager<Realtor> userManager, RoleManager<IdentityRole> roleManager, IAgency agency)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_agency = agency;
		}
		
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Realtor>>> GetUsers()
		{
			var users = await _userManager.Users
			.Include(u => u.Agency)
			.Include(r => r.ResidenceList)
			.ToListAsync();

			return Ok(users);
		}

		[HttpPost("register")]
		public async Task<IActionResult> RegisterRealtor(RealtorDto model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			
			var agency = await _agency.GetByIdAsync(model.Agency.AgencyId);
			if (agency == null)
			{
				return BadRequest("Agency not found");
			}

			
			var realtor = new Realtor
			{
				UserName = model.Email,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				PhoneNumber = model.PhoneNumber,
				Picture = model.Picture,
				Roles = model.Roles,
				Agency = agency
			};

			// Hash the password
			var passwordHasher = new PasswordHasher<Realtor>();
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

				return CreatedAtAction("GetUsers", new { id = realtor.Id, realtor.Email }, realtor);
			}
			else
			{

				return BadRequest(ModelState); // Return ModelState with errors
			}
		}
		
		[HttpGet("{id}")]
		public async Task<ActionResult<Realtor>> GetUsersById(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			
			return user;
		}
		
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(string id, RealtorDto model)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.Email = model.Email;
			user.PhoneNumber = model.PhoneNumber;
			user.Picture = model.Picture;
			user.Roles = model.Roles;

			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return NoContent();
			}
			else
			{
				return BadRequest(result.Errors);
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
