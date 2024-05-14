
//author: Johan Krångh
namespace FribergWebAPI.Models
{
	public class Municipality
	{
		public int Id { get; set; }
		public string MunicipalityName { get; set; }
		public ICollection<Residence>? Residences { get; set; }

	}
}
