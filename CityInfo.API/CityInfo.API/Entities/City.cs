using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class City
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        public string name { get; set; }
        [MaxLength(200)]
        public string? description { get; set; }
        public ICollection<PointOfInterest> pointsOfInterest { get; set; }
            = new List<PointOfInterest>();

        public City(string name)
        {
            this.name = name;
        }
    }
}
