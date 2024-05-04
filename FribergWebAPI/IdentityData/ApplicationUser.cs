using FribergWebAPI.Constants;
using Microsoft.AspNetCore.Identity;

namespace FribergWebAPI.IdentityData
{
	public class ApplicationUser : IdentityUser
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set;}
        public List<string>? Roles { get; set; } 
	}
}