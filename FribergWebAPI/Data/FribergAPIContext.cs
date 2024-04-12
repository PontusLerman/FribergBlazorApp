﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FribergWebAPI.Models;

namespace FribergWebAPI.Data
{
    public class FribergAPIContext : DbContext
    {
        public FribergAPIContext (DbContextOptions<FribergAPIContext> options)
            : base(options)
        {
        }

        public DbSet<FribergWebAPI.Models.Agency> Agency { get; set; } = default!;
    }
}