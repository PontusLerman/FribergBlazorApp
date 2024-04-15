
namespace FribergWebAPI.Models
{
    public class Residence
    {
        public int Id { get; set; }
        public string? Category { get; set; }
        public string? Address { get; set; }
        public string? Municipality { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal LivingArea { get; set; }
        public decimal BiArea { get; set; }
        public decimal PlotArea { get; set; }
        public string? ObjectDescription { get; set; }
        public int NumberOfRooms { get; set; }
        public decimal MonthlyFee { get; set; }
        public decimal OperatingCostPerYear { get; set; }
        public int ConstructionYear { get; set; }
    }
}