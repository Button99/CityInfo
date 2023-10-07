using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController: ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> getCities()
        {
            var cities = new JsonResult(CitiesDataStore.Current.Cities);
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> getCity(int id)
        {
            var city = new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.id == id));
            if(city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}
