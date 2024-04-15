
﻿using System;
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

        //Pontus
        public DbSet<Realtor> realtors { get; set; }
        public DbSet<FribergWebAPI.Models.Agency> Agency { get; set; } = default!;
    }
}
