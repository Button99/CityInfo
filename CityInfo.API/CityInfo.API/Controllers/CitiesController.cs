using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController: ControllerBase
    {
        private readonly CitiesDataStore _citiesDataStore;
        public CitiesController(CitiesDataStore citiesDataStore)
        {
            _citiesDataStore = citiesDataStore;
        }

        public CitiesDataStore CitiesDataStore { get; }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> getCities()
        {
            var cities = new JsonResult(_citiesDataStore.Cities);
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> getCity(int id)
        {
            var city = new JsonResult(_citiesDataStore.Cities.FirstOrDefault(c => c.id == id));
            if(city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}
