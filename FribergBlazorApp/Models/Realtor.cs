namespace FribergBlazorApp.Models
{
    public class Realtor
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string picture { get; set; }
        public Agency agency { get; set; }
    }
}
