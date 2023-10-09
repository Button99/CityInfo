using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        public string name { get; set; }
        [ForeignKey("cityId")]
        public City? City { get; set; }

        public PointOfInterest(string name)
        {
            this.name = name;
        }
    }
}
