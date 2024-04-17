namespace FribergWebAPI.Models
{
    public class ResidencePicture
    {
        public int Id { get; set; }
        public string Picture { get; set; }
        public Residence Residence { get; set; }
    }
}
