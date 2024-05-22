//author: Christian Alp
namespace FribergWebAPI.DTOs
{
    public class RealtorCreateAgency : AgencyDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Picture { get; set; }
    }
}
