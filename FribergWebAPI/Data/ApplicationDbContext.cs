using Microsoft.EntityFrameworkCore;
using FribergWebAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using FribergWebAPI.Constants;

namespace FribergWebAPI.Data
{
	public class ApplicationDbContext : IdentityDbContext<Realtor>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
		{ 
		}
	
		//author: Pontus Lerman
		public DbSet<Realtor> Realtors { get; set; }
		
		//author: Johan Krångh
		public DbSet<Agency> Agency { get; set; } = default!;
		public DbSet<Municipality> Municipality { get; set; } = default!;
		
		//author: Christian Alp
		public DbSet<Residence> Residences { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ResidencePicture> ResidencePicture { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			
			modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
			{
				Name = ApiRoles.DefaultRealtor,
				NormalizedName = ApiRoles.DefaultRealtor,
				Id = "1"
			},
			new IdentityRole
			{
				Name = ApiRoles.SuperRealtor,
				NormalizedName = ApiRoles.SuperRealtor,
				Id = "2"
			});
			
			//This function makes it so that it doesn't delete it's childrens or parents.
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
			.HasOne(e => e.Category)
			.WithMany(f => f.Residences)
			.OnDelete(DeleteBehavior.Restrict);
			
			modelBuilder.Entity<Residence>()
			.HasOne(e => e.Municipality)
			.WithMany(f => f.Residences)
			.OnDelete(DeleteBehavior.Restrict);
			
			modelBuilder.Entity<Residence>()
			.HasOne(r => r.Realtor)
			.WithMany(r => r.ResidenceList)
			.OnDelete(DeleteBehavior.Restrict);
		}
	}
}