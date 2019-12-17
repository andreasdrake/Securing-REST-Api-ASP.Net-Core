using LandonApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandonApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class InfoController:ControllerBase
    {
        private readonly HotelInfo hotelInfo;

        public InfoController(IOptions<HotelInfo> hotelInfo)
        {
            this.hotelInfo = hotelInfo.Value;
        }

        [HttpGet(Name = nameof(GetInfo))]
        [ProducesResponseType(200)]
        public ActionResult<HotelInfo> GetInfo()
        {
            hotelInfo.Href = Url.Link(nameof(GetInfo), null);

            return hotelInfo;
        }
    }
}
