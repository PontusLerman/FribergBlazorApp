namespace FribergWebAPI.DTOs
{
    public class RealtorDto
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Picture { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
        public RealtorAgencyDto Agency { get; set; }   
    }
}
