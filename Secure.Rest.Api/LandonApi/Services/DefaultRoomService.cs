﻿using AutoMapper;
using LandonApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandonApi.Services
{
    public class DefaultRoomService : IRoomService
    {
        private readonly HotelApiDbContext _context;
        private readonly IMapper _mapper;

        public DefaultRoomService(HotelApiDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<Room> GetRoomAsync(Guid id)
        {
            var entity = await _context.Rooms.SingleOrDefaultAsync(x =>
                     x.Id == id);

            if (entity == null)
            {
                return null;
            }
            return _mapper.Map<Room>(entity);
            //return new Room
            //{
            //    Href = null,
            //    Name = entity.Name,
            //    Rate = entity.Rate / 100.0m
            //};
        }
    }
}
