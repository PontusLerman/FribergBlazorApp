
//author: Christian Alp
namespace FribergWebAPI.DTOs
{
    public class AuthResponseDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
