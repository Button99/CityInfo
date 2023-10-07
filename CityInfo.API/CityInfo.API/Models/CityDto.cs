namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? description { get; set;}

        public int numberOfPointsOfInterest
        {
            get { return numberOfPointsOfInterest; }
        }
        public ICollection<PointOfInterestDto> pointsOfInterest { get ; set; }
            = new List<PointOfInterestDto>();
        
    }
}
