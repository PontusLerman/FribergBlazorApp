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

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterRealtor(RegisterRealtorDto model)
        {
            // Validate model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create a new ApplicationUser (assuming ApplicationUser represents your realtor model)
            var realtor = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            // Hash the password
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            realtor.PasswordHash = passwordHasher.HashPassword(realtor, model.Password);

            // Save the realtor to the database
            var result = await _userManager.CreateAsync(realtor, model.Password);

            if (result.Succeeded)
            {
                // Return success response
                return Ok(new { realtor.Id, realtor.Email });
            }
            else
            {
                // Return error response
                return BadRequest(result.Errors);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }
    }
}
