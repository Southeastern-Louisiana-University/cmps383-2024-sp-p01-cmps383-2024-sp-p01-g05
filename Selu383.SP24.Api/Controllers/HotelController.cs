using Microsoft.AspNetCore.Mvc;

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
    }

}
