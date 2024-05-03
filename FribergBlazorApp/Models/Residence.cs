namespace FribergBlazorApp.Models
{

    public class Residence
    {
        public int id { get; set; }
        public Category category { get; set; }
        public string address { get; set; }
        public Municipality municipality { get; set; }
        public decimal startingPrice { get; set; }
        public decimal livingArea { get; set; }
        public decimal biArea { get; set; }
        public decimal plotArea { get; set; }
        public string? objectDescription { get; set; }
        public int numberOfRooms { get; set; }
        public decimal monthlyFee { get; set; }
        public decimal operatingCostPerYear { get; set; }
        public int constructionYear { get; set; }
        public Picture[] pictures { get; set; }
        public Realtor realtor { get; set; }
    }    

}
