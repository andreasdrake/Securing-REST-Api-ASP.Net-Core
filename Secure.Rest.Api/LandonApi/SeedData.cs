using LandonApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandonApi
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await AddTestData(services.GetRequiredService<HotelApiDbContext>());
        }

        public static async Task AddTestData(HotelApiDbContext context)
        {
            if (context.Rooms.Any())
            {
                return;
            }

            context.Rooms.Add(
                new RoomEntity
                {
                    Id = Guid.Parse("bf22514b-cc86-409b-85ab-d5def998cf12"),
                    Name="Oxford Suite",
                    Rate=10119
                });

            context.Rooms.Add(
              new RoomEntity
              {
                  Id = Guid.Parse("6414ee10-8958-484a-8212-8e7f7200b8f7"),
                  Name = "Driscoll Suite",
                  Rate = 23959
              });

            await context.SaveChangesAsync();
        }
    }
}
