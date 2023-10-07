using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsOfInterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> getPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.pointsOfInterest);
        }

        [HttpGet("{pointOfInterestId}", Name = "getPointOfInterest")]
        public ActionResult<PointOfInterestDto> getPointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.id == cityId);
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
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.id == cityId);
            if(city == null)
            {
                return NotFound();
            }
            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.pointsOfInterest).Max(p => p.id);
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
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.id == cityId);
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
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.id == cityId);
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
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.id == cityId);
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
            return NoContent();
        }

    }
}
