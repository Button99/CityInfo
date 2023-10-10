namespace CityInfo.API.Models
{
    public class CityWithoutPointOfInterestDto
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? description { get; set; }
    }
}
