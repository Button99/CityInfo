using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name for the value")]
        [MaxLength(50)]
        public string name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? description { get; set; }
    }
}
