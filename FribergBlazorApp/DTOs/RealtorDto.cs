
//author: Christian Alp, Johan Krångh, Pontus Lerman
using System.ComponentModel.DataAnnotations;

namespace FribergBlazorApp.DTOs
{
	public class RealtorDto : LoginRealtorDto
	{
		public string? Id { get; set; }

        [Required(ErrorMessage = "Förnamn är obligatoriskt.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn är obligatoriskt.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Telefonnummer är obligatoriskt.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Telefonnummer får endast innehålla siffror.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Bild är obligatoriskt.")]
        public string Picture { get; set; }

		public List<string> Roles { get; set; }
		public RealtorAgencyDto Agency { get; set; }   
	}
}
