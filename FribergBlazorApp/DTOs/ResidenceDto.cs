using System.ComponentModel.DataAnnotations;

//author: Christian Alp, Johan Krångh, Pontus Lerman
namespace FribergBlazorApp.DTOs
{
	public class ResidenceDto
	{
         public int Id { get; set; }

        [Required(ErrorMessage = "Kategori är obligatoriskt.")]
        public CategoryDto Category { get; set; }

        [Required(ErrorMessage = "Adress är obligatoriskt.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Kommun är obligatoriskt.")]
        public MunicipalityDto Municipality { get; set; }

        [Required(ErrorMessage = "Utgångspris är obligatoriskt.")]
        public decimal StartingPrice { get; set; }

        [Required(ErrorMessage = "Boyta är obligatoriskt.")]
        public decimal LivingArea { get; set; }

        [Required(ErrorMessage = "Biyta är obligatoriskt.")]
        public decimal BiArea { get; set; }

        [Required(ErrorMessage = "Tomtstorlek är obligatoriskt.")]
        public decimal PlotArea { get; set; }

        public string ObjectDescription { get; set; }

        [Required(ErrorMessage = "Antal rum är obligatoriskt.")]
        public int NumberOfRooms { get; set; }

        [Required(ErrorMessage = "Månadskostnad är obligatoriskt.")]
        public decimal MonthlyFee { get; set; }

        [Required(ErrorMessage = "Driftkostnad är obligatoriskt.")]
        public decimal OperatingCostPerYear { get; set; }

        [Required(ErrorMessage = "Byggår är obligatoriskt.")]
        public int ConstructionYear { get; set; }

        public virtual List<PictureDto> Pictures { get; set; }

        [Required(ErrorMessage = "Mäklare är obligatoriskt.")]
        public RealtorDto Realtor { get; set; }
	}
}
