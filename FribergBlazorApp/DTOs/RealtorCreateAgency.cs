//author: Christian Alp
using System.ComponentModel.DataAnnotations;

namespace FribergBlazorApp.DTOs
{
    public class RealtorCreateAgency : AgencyDto
    {
        [Required(ErrorMessage = "Förnamn är obligatoriskt.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn är obligatoriskt.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email är obligatoriskt.")]
        [EmailAddress(ErrorMessage = "Ogiltig email-adress.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lösenord är obligatoriskt.")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Telefonnummer är obligatoriskt.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Telefonnummer får endast innehålla siffror.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Bild är obligatoriskt.")]
        public string Picture { get; set; }
    }
}
