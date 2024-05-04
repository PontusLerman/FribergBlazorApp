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
		public DbSet<ResidencePicture> ResidencePicture { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Residence>()
			.HasOne(e => e.Category)
			.WithMany(f => f.Residences)
			.OnDelete(DeleteBehavior.Restrict);
			
			modelBuilder.Entity<Realtor>()
			.HasOne(r => r.Agency)
			.WithMany(a => a.Employees)
			.OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<Realtor>()
      .HasMany(r => r.ResidenceList)
      .WithOne(a => a.Realtor)
      .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Municipality>()
			.HasMany(m => m.Residences)
			.WithOne(r => r.Municipality)
			.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Residence>()
			.HasOne(e => e.Municipality)
			.WithMany(f => f.Residences)
			.OnDelete(DeleteBehavior.Restrict);
			//Supposed to hinder deletion of municipalities in use on
			//a recidence, doesnt work
			modelBuilder.Entity<Residence>()
				.HasOne(r => r.Realtor)
				.WithMany(r => r.ResidenceList)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}