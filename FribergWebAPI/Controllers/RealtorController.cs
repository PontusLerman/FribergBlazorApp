using AutoMapper;
using FribergWebAPI.Constants;
using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.Data.Repositories;
using FribergWebAPI.DTOs;
using FribergWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
		private readonly IConfiguration _configuration;

		public RealtorController(UserManager<Realtor> userManager, RoleManager<IdentityRole> roleManager, IAgency agency, IMapper mapper, IConfiguration configuration)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_agency = agency;
			_mapper = mapper;
			_configuration = configuration;
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

        [HttpGet]
        [Route("realtors-by-agency/{agencyId}")]
        public async Task<ActionResult<IEnumerable<RealtorDto>>> GetRealtorsByAgency(int agencyId)
        {
			var realtors = await _userManager.Users
				.Include(r=>r.Agency)
				.Include(r=>r.ResidenceList)
				.Where(r => r.Agency.AgencyId == agencyId)
				.Where(r=>r.Approved == true)
				.ToListAsync();
            var realtorDtos = _mapper.Map<List<RealtorDto>>(realtors);
            return Ok(realtorDtos);
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
			realtor.Approved = model.Approved;
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

		[HttpPost]
		[Route("register/user-with-existing-agency")]
		public async Task<ActionResult<Realtor>> RegisterRealtor(RealtorDto model)
		{
			try
			{
				var agency = await _agency.GetByIdAsync(model.Agency.AgencyId);
				if (agency == null)
				{
					return BadRequest("Agency not found, Try create a new agency first");
				}
				
				if (await _userManager.Users.AnyAsync(r => r.PhoneNumber == model.PhoneNumber))
				{
					return BadRequest("Phone number already exists.");
				}			

				Realtor realtor = new Realtor()
				{
					UserName = model.Email,
					Email = model.Email,
					FirstName = model.FirstName,
					LastName = model.LastName,
					PhoneNumber = model.PhoneNumber,
					Picture = model.Picture,
					Agency = agency,
					Roles = new List<string> { "DefaultRealtor" },
					Approved = false
				};


				// Hash the password
				var passwordHasher = new PasswordHasher<Realtor>();
				realtor.PasswordHash = passwordHasher.HashPassword(realtor, model.Password);

				var result = await _userManager.CreateAsync(realtor, model.Password);
				var concatenatedErrors = string.Join(" ; ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));


				if (result.Succeeded)
				{
					var role = await _roleManager.FindByNameAsync("DefaultRealtor");
					if (role != null)
					{
						await _userManager.AddToRoleAsync(realtor, role.Name);
						await _userManager.AddClaimAsync(realtor, new Claim(ClaimTypes.Role, role.Name));
					}

					return CreatedAtAction("GetUsers", new { id = realtor.Id, realtor.Email }, realtor);
				}
				else
				{
					return BadRequest(concatenatedErrors);
				}

			}
			catch
			{
				return Problem($"An error occurred: {nameof(RegisterRealtor)}", statusCode: 500);
			}
		}

		[HttpPost]
		[Route("register/user-with-new-agency")]
		public async Task<ActionResult<Realtor>> RegisterUserWithNewAgency(RealtorCreateAgency model)
		{
			try
			{			
				if (await _userManager.Users.AnyAsync(r => r.PhoneNumber == model.PhoneNumber))
				{
					return BadRequest("Phone number already exists.");
				}
				
				var newAgency = new Agency	
				{
					//AgencyId = model.AgencyId,
					AgencyName = model.AgencyName,
					AgencyLogoURL = model.AgencyLogoURL,
					AgencyDescription = model.AgencyDescription
				};

				await _agency.AddAsync(newAgency);

				var realtor = new Realtor()
				{
					UserName = model.Email,
					Email = model.Email,
					FirstName = model.FirstName,
					LastName = model.LastName,
					PhoneNumber = model.PhoneNumber,
					Picture = model.Picture,
					Agency = newAgency,
					Roles = new List<string> { "SuperRealtor" },
					Approved = true
				};

				// Hash the password
				var passwordHasher = new PasswordHasher<Realtor>();
				realtor.PasswordHash = passwordHasher.HashPassword(realtor, model.Password);

				var result = await _userManager.CreateAsync(realtor, model.Password);

				if (result.Succeeded)
				{
					var role = await _roleManager.FindByNameAsync("SuperRealtor");
					if (role != null)
					{
						await _userManager.AddToRoleAsync(realtor, role.Name);
						await _userManager.AddClaimAsync(realtor, new Claim(ClaimTypes.Role, role.Name));
					}

					return CreatedAtAction("GetUsers", new { id = realtor.Id, realtor.Email }, realtor);
				}
				else
				{
					return BadRequest(ModelState);
				}
			}
			catch (Exception ex)
			{
				return Problem($"An error occurred: {ex.Message}", statusCode: 500);
			}
		}

		[HttpPost]
		[Route("login")]
		public async Task<ActionResult<AuthResponseDto>> LoginRealtor(LoginRealtorDto model)
		{
			try
			{
				var realtor = await _userManager.FindByEmailAsync(model.Email);
				var passwordValid = await _userManager.CheckPasswordAsync(realtor, model.Password);

				if (realtor == null || passwordValid == false)
				{
					return Unauthorized(model);
				}

				string tokenString = await GenerateToken(realtor);

				var response = new AuthResponseDto
				{
					Email = model.Email,
					Token = tokenString,
					Id = realtor.Id,
					Roles = (List<string>)realtor.Roles,
					FirstName = realtor.FirstName
				};

				return Ok(response);
			}
			catch
			{
				return Problem($"An error occurred: {nameof(LoginRealtor)}", statusCode: 500);
			}
		}

		private async Task<string> GenerateToken(Realtor realtor)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var realtorClaims = await _userManager.GetClaimsAsync(realtor);

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, realtor.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, realtor.Email),
				new Claim(ApiClaims.Rid, realtor.Id),
				new Claim(ClaimTypes.NameIdentifier, realtor.Id)
			}.Union(realtorClaims);

			var token = new JwtSecurityToken(
				issuer: _configuration["JwtSettings:Issuer"],
				audience: _configuration["JwtSettings:Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
