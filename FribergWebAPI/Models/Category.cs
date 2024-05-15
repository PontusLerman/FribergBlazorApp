
//author: Christian
namespace FribergWebAPI.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		
		public ICollection<Residence>? Residences { get; set; }
	}
}

