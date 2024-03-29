﻿using Microsoft.AspNetCore.Mvc;
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

            var hoteltoReturn = new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Address = hotel.Address,
            };

            return CreatedAtAction(nameof(GetbyId), new { id = hoteltoReturn.Id}, hoteltoReturn );
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
            if (hotelDto.Name.Length > 120)
            {
                return BadRequest(hotelDto);
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
        [HttpDelete("{id:int}")]
        public IActionResult DeleteHotel([FromRoute] int id)
        {
            var hoteltoDelete = _dataContext.Hotels
                .FirstOrDefault(x => x.Id == id);

            if (hoteltoDelete == null)
            {
                return NotFound();
            }
            _dataContext.Hotels.Remove(hoteltoDelete);
            _dataContext.SaveChanges();

            var hoteltoReturn = new HotelDto
            {
                Id = hoteltoDelete.Id,
                Name = hoteltoDelete.Name,
                Address = hoteltoDelete.Address,
            };

            return Ok(hoteltoReturn);
        }

    }

}
