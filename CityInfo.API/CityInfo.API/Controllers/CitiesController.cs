using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Authorize]

    [ApiController]
    [Route("api/cities")]
    public class CitiesController: ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        public CitiesDataStore CitiesDataStore { get; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> getCities()
        {
            var cities = await _cityInfoRepository.getCitiesAsync();
            var results = new List<CityWithoutPointOfInterestDto>();
            foreach(var city in cities)
            {
                results.Add(new CityWithoutPointOfInterestDto
                {
                    id = city.id,
                    description = city.description,
                    name = city.name
                });
            }
            return Ok(cities);
        }

/*        [HttpGet("{id}")]
        public ActionResult<CityDto> getCity(int id)
        {
            var city = new JsonResult(_citiesDataStore.Cities.FirstOrDefault(c => c.id == id));
            if(city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }*/
    }
}
