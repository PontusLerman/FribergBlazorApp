using System.ComponentModel.DataAnnotations;

//author: Christian Alp
namespace FribergWebAPI.DTOs
{
    public class LoginRealtorDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
