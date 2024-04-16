using Microsoft.EntityFrameworkCore;
using FribergWebAPI.Models;

namespace FribergWebAPI.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options){ }
	
		//author: Pontus
		public DbSet<Realtor> Realtors { get; set; }
		
		//author: Johan
		public DbSet<Agency> Agency { get; set; } = default!;
		public DbSet<Municipality> Municipality { get; set; } = default!;
		
		//author: Christian
		public DbSet<Residence> Residences { get; set; }
		public DbSet<Category> Categories { get; set; }
	}
}