using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;
        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
        }

        public async Task<IEnumerable<City>> getCitiesAsync()
        {
            return await _context.cities.OrderBy(c => c.name).ToListAsync();
        }

        public async Task<City?> getCityAsync(int cityId, bool includePointsOfInterest)
        {
            if(includePointsOfInterest)
            {
                return await _context.cities.Include(c => c.pointsOfInterest)
                    .Where(c => c.id == cityId).FirstOrDefaultAsync();
            }

            return await _context.cities
                .Where(c => c.id == cityId).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> getPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await _context.pointOfInterests.Where(p => p.City.id == cityId).ToListAsync();
        }

        public Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            throw new NotImplementedException();
        }
    }
}
