using FribergBlazorApp.Models;

//author: Christian Alp, Johan Krångh, Pontus Lerman
namespace FribergBlazorApp.DTOs
{
	public class ResidenceDto
	{
        public int Id { get; set; }
        public CategoryDto Category { get; set; }
		public string? Address { get; set; }
		public MunicipalityDto Municipality { get; set; }
		public decimal StartingPrice { get; set; }
		public decimal LivingArea { get; set; }
		public decimal BiArea { get; set; }
		public decimal PlotArea { get; set; }
		public string? ObjectDescription { get; set; }
		public int NumberOfRooms { get; set; }
		public decimal MonthlyFee { get; set; }
		public decimal OperatingCostPerYear { get; set; }
		public int ConstructionYear { get; set; }
		public virtual List<Picture> Pictures { get; set; }
		public RealtorDto Realtor { get; set; }
	}
}
