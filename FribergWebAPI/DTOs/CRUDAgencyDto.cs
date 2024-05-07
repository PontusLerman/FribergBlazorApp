namespace FribergWebAPI.DTOs
{
    public class CRUDAgencyDto
    {
        public int Id { get; set; }
        public string AgencyName { get; set; }
        public string AgencyDescription { get; set; }
        public string? AgencyLogoURL { get; set; } = "";
    }
}
