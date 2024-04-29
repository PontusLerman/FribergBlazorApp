namespace FribergBlazorApp.Models
{

    public class Municipality
    {
        public int id { get; set; }
        public string municipalityName { get; set; }
        public Residence[] residences { get; set; }
    }
}
