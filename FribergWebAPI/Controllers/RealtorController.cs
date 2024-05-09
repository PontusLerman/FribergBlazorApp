using AutoMapper;
using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.DTOs;
using FribergWebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//author: Christian Alp, Pontus Lerman
namespace FribergWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RealtorController : ControllerBase
	{
		private readonly UserManager<Realtor> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IAgency _agency;
		private readonly IMapper _mapper;

		public RealtorController(UserManager<Realtor> userManager, RoleManager<IdentityRole> roleManager, IAgency agency, IMapper mapper)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_agency = agency;
			_mapper = mapper;
		}
		
		[HttpGet]
		public async Task<ActionResult<IEnumerable<RealtorDto>>> GetUsers()
		{
			var realtors = await _userManager.Users
			.Include(u => u.Agency)
			.Include(r => r.ResidenceList)
			.ToListAsync();
			var realtorDtos = _mapper.Map<List<RealtorDto>>(realtors);

			return Ok(realtorDtos);
		}
		
		[HttpGet("{id}")]
		public async Task<ActionResult<RealtorDto>> GetUsersById(string id)
		{
			var realtors = await _userManager.Users
			.Include(u => u.Agency)
			.Include(r => r.ResidenceList)
			.FirstOrDefaultAsync(i => i.Id == id);
			
			var realtorDtos = _mapper.Map<RealtorDto>(realtors);

			if (realtors == null)
			{
				return NotFound();
			}
			
			return Ok(realtorDtos);
		}

		[HttpPost("register")]
		public async Task<ActionResult<Realtor>> RegisterRealtor(RealtorDto model)
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
		
		[HttpPut("{id}")]
		public async Task<ActionResult<Realtor>> UpdateUser(string id, RealtorDto model)
		{
			var realtor = await _userManager.FindByIdAsync(id);
			if (realtor == null)
			{
				return NotFound();
			}
			
			var agency = await _agency.GetByIdAsync(model.Agency.AgencyId);
			if (agency == null)
			{
				return BadRequest("Agency not found");
			}
			
			realtor.FirstName = model.FirstName;
			realtor.LastName = model.LastName;
			realtor.UserName = model.Email;
			realtor.Email = model.Email;
			realtor.PhoneNumber = model.PhoneNumber;
			realtor.Picture = model.Picture;
			realtor.Roles = model.Roles;
			realtor.Agency = agency;

			var result = await _userManager.UpdateAsync(realtor);
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
