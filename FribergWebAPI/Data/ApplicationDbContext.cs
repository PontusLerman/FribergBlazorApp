using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FribergWebAPI.Models;

namespace FribergWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options){ }

        //author: Pontus
        public DbSet<Realtor> realtors { get; set; }
        //author: Johan
        public DbSet<FribergWebAPI.Models.Agency> Agency { get; set; } = default!;
        //author: Christian
        public DbSet<Residence> Residences { get; set; }
        public DbSet<FribergWebAPI.Models.Municipality> Municipality { get; set; } = default!;
    }
}
