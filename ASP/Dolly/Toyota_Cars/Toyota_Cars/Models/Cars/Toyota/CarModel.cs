using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplicationGeo.Models.Cars.Toyota;

namespace Toyota_Cars.Models.Cars.Toyota
{
    public class CarModel
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        public int ColorId { get; set; }

        [ForeignKey("ColorId")]
        public ColorModel Color { get; set; }

        [Required]
        public int ConfigurationId { get; set; }

        [ForeignKey("ConfigurationId")]
        public ConfigurationModel Configuration { get; set; }
    }
}