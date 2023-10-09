namespace CityInfo.API.Models
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
     //   public static CitiesDataStore Current { get;  }= new CitiesDataStore();
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    id= 1,
                    name= "Athens",
                    description= "Athens City!",
                    pointsOfInterest= new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            id= 1,
                            name= "Akropolis",
                            description= "Akropolis"
                        },
                         new PointOfInterestDto()
                        {
                            id= 2,
                            name= "Syntagma square",
                            description= "Syntagma sq."
                        }
                    }

                },
                new CityDto()
                {
                    id= 2,
                    name= "London",
                    description= "London city",
                    pointsOfInterest= new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            id= 1,
                            name= "London Bridge",
                            description= "London Bridge"
                        },
                         new PointOfInterestDto()
                        {
                            id= 2,
                            name= "Palace",
                            description= "Palace"
                        }
                    }
                },
                new CityDto()
                {
                    id= 3,
                    name= "NYC",
                    description= "NYC city",
                    pointsOfInterest= new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            id= 1,
                            name= "NY Pizza",
                            description= "NYC Pizza is great"
                        },
                         new PointOfInterestDto()
                        {
                            id= 2,
                            name= "NY Knicks",
                            description= "NYC Knicks"
                        }
                    }
                }
            };
        }
    }
}
