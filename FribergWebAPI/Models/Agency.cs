
//author: Johan Krångh
namespace FribergWebAPI.Models
{
	public class Agency 
	{
		public int AgencyId { get; set; }
		public string AgencyName { get; set; }
		public string AgencyDescription { get; set; }
		public string? AgencyLogoURL { get; set; } = "";
		public virtual List<Realtor> Employees { get; set;}
	}
}
