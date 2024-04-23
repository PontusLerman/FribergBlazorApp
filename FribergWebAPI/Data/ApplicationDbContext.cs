﻿using Microsoft.EntityFrameworkCore;
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
			.WithMany(u => u.Residences)
			.OnDelete(DeleteBehavior.Restrict);
			
			modelBuilder.Entity<Realtor>()
			.HasOne(r => r.Agency)
			.WithMany(a => a.Employees)
			.OnDelete(DeleteBehavior.Restrict);
		}
	}
}