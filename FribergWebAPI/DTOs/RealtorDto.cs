using Microsoft.AspNetCore.Identity;

//author: Christian Alp, Johan Krångh, Pontus Lerman
namespace FribergWebAPI.DTOs
{
	public class RealtorDto
	{
		public string? Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Picture { get; set; }
		public string Password { get; set; }
		public List<string> Roles { get; set; }
		public RealtorAgencyDto Agency { get; set; }   
	}
}
