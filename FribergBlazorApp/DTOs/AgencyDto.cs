
//author: Christian Alp, Johan Krångh, Pontus Lerman
namespace FribergWebAPI.DTOs
{
	public class AgencyDto
	{
		public int AgencyId { get; set; }
		public string AgencyName { get; set; }
		public string AgencyDescription { get; set; }
		public string? AgencyLogoURL { get; set; } = "";
		public List<RealtorDto>? Employees { get; set; }
	}
}
