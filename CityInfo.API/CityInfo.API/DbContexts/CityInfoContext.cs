using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext: DbContext
    {
        public DbSet<City> cities { get; set; } = null!;
        public DbSet<PointOfInterest> pointOfInterests { get; set; } = null!;
        
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options) {
            
        }
/*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("connectionstring");
            base.OnConfiguring(optionsBuilder);
        }*/
    }
}
