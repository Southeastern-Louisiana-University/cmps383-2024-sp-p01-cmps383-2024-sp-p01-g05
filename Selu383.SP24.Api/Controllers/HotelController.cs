using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Selu383.SP24.Api.Controllers
{
    [ApiController]
    [Route("/api/hotels")]
    public class HotelController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public HotelController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var hotels = _dataContext.Hotels.
                Select(x => new HotelDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                })
                .ToList();

            return Ok(hotels);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetbyId(int id) 
        {
            var targetHotel = _dataContext.Set<Hotel>()
                .FirstOrDefault(x => x.Id == id);

            if (targetHotel == null)
            {
                return NotFound();
            }

            var hoteltoReturn = new HotelDto
            {
                Id = id,
                Name = targetHotel.Name,
                Address = targetHotel.Address,
            };
            return Ok(hoteltoReturn);
            
        }
        [HttpPost]
        public IActionResult CreateHotel(HotelCreateDto hotelDto)
        {
            if(hotelDto.Name.Length > 120)
            {
                return BadRequest(hotelDto);
            }
            var hotel = new Hotel {
                
            Name = hotelDto.Name,
            Address = hotelDto.Address,

            };
            
            _dataContext.Set<Hotel>().Add(hotel);
            _dataContext.SaveChanges();

            return Ok(hotelDto);
        }

        [HttpPut ("{id:int}")]
        public IActionResult PutHotel([FromBody]HotelUpdateDto hotelDto, [FromRoute] int id)
        {
            var targetHotel = _dataContext.Set<Hotel>()
                .FirstOrDefault(x => x.Id == id);

            if (targetHotel == null)
            {
                return NotFound();
            }
            targetHotel.Name = hotelDto.Name;
            targetHotel.Address = hotelDto.Address;

            _dataContext.SaveChanges();

            var hoteltoReturn = new HotelDto
            {
                Id = targetHotel.Id,
                Name = targetHotel.Name,
                Address = targetHotel.Address,

            };
            return Ok(hoteltoReturn);
        }
    }

}
