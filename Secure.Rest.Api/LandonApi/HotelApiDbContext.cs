﻿using LandonApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandonApi
{
    public class HotelApiDbContext:DbContext
    {
        public HotelApiDbContext(DbContextOptions options)
            :base(options)  { }

        public DbSet<RoomEntity> Rooms { get; set; }
    }
}
