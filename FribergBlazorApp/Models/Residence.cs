namespace FribergBlazorApp.Models
{

    public class Residence
    {
        public int id { get; set; }
        public Category category { get; set; }
        public string address { get; set; }
        public Municipality municipality { get; set; }
        public int startingPrice { get; set; }
        public int livingArea { get; set; }
        public int biArea { get; set; }
        public int plotArea { get; set; }
        public string objectDescription { get; set; }
        public int numberOfRooms { get; set; }
        public int monthlyFee { get; set; }
        public int operatingCostPerYear { get; set; }
        public int constructionYear { get; set; }
        public Picture[] pictures { get; set; }
        public Realtor realtor { get; set; }
    }    

}
