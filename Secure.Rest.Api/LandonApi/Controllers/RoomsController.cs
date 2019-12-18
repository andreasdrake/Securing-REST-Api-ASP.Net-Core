using AutoMapper;
using LandonApi.Models;
using LandonApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandonApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class RoomsController:ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
          }

        [HttpGet(Name = nameof(GetRooms))]
        public IActionResult GetRooms()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{roomId}", Name = nameof(GetRoomById))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Room>> GetRoomById(Guid roomId)
        {
            var room = await _roomService.GetRoomAsync(roomId);
            if (room == null)
            {
                return NotFound();
            }
            
            
            return room;
            //var resource = new Room
            //{
            //    Href = Url.Link(nameof(GetRoomById), new { roomId = entity.Id }),
            //    Name = entity.Name,
            //    Rate = entity.Rate / 100.0m
            //};

            //return resource;
        }
    }
}
