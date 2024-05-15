
//author: Christian Alp, Johan Krångh, Pontus Lerman
namespace FribergBlazorApp.DTOs
{
	public class RealtorDto : LoginRealtorDto
	{
		public string? Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string Picture { get; set; }
		public List<string> Roles { get; set; }
		public RealtorAgencyDto Agency { get; set; }   
	}
}
