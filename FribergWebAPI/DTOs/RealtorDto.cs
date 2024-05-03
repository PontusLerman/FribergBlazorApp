namespace FribergWebAPI.DTOs
{
    public class RealtorDto
    {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Picture { get; set; }
            public RealtorAgencyDto Agency { get; set; }   
    }
}
