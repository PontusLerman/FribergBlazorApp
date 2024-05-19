using System.ComponentModel.DataAnnotations;

//author: Christian Alp
namespace FribergBlazorApp.DTOs
{
    public class LoginRealtorDto
    {
        [Required(ErrorMessage = "Email är obligatoriskt.")]
        [EmailAddress(ErrorMessage = "Ogiltig email-adress.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lösenord är obligatoriskt.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
