using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsOfInterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly ILocalMailService _mailService;
        private readonly CitiesDataStore _citiesDataStore;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            ILocalMailService mailService, CitiesDataStore citiesDataStore)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentException(nameof(mailService));
            _citiesDataStore = citiesDataStore;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> getPointsOfInterest(int cityId)
        {
            try
            {
                var city = _citiesDataStore.Cities.FirstOrDefault(c => c.id == cityId);
                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} wasnt found when accessing points of interest.");
                    return NotFound();
                }
                return Ok(city.pointsOfInterest);
            } catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city {cityId}", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpGet("{pointOfInterestId}", Name = "getPointOfInterest")]
        public ActionResult<PointOfInterestDto> getPointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.pointsOfInterest.FirstOrDefault(c => c.id == pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }

        [HttpPost] 
        public ActionResult<PointOfInterestDto> createPointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterest)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.id == cityId);
            if(city == null)
            {
                return NotFound();
            }
            var maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(c => c.pointsOfInterest).Max(p => p.id);
            var finalPointOfInterest = new PointOfInterestDto()
            {
                id = ++maxPointOfInterestId,
                name = pointOfInterest.name,
                description = pointOfInterest.description
            };
            city.pointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("getPointOfInterest", new
            {
                cityId = cityId,
                pointOfInterest = finalPointOfInterest.id
            }, finalPointOfInterest);
        }

        [HttpPut]
        public ActionResult<PointOfInterestDto> updatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.id == cityId);
            if(city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.pointsOfInterest.FirstOrDefault(c => c.id == pointOfInterestId);
            if(pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.name = pointOfInterest.name;
            pointOfInterest.description = pointOfInterest.description;

            return NoContent();
        }

        [HttpPatch("{pointOfInterestId}")]
        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
                        JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.id == cityId);
            if(city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.pointsOfInterest.FirstOrDefault(c => c.id == pointOfInterestId);
            if(pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                name = pointOfInterestFromStore.name,
                description = pointOfInterestFromStore.description
            };

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.name = pointOfInterestToPatch.name;
            pointOfInterestFromStore.description = pointOfInterestToPatch.description;

            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.id == cityId);
            if(city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.pointsOfInterest.FirstOrDefault(c => c.id == pointOfInterestId);
            if(pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            city.pointsOfInterest.Remove(pointOfInterestFromStore);
            _mailService.send("Point of interest deleted.", $"Point of interest deleted {pointOfInterestFromStore.name} with id {pointOfInterestFromStore.id}");
            return NoContent();
        }

    }
}
