using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationGeo_2.Models.Entities.Geo;
using System.ComponentModel.DataAnnotations.Schema;

public class CountryModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "The Country Name is required.")]
    [StringLength(64, MinimumLength = 3, 
        ErrorMessage = "The Country Name must be between 3 and 64 characters long.")]
    [DisplayName("Country Name")]
    public string Name { get; set; }
    
    public int? CapitalId { get; set; }
    
    [ForeignKey("CapitalId")]
    public CityModel? Capital { get; set; }


    public ICollection<AreaModel> Areas { get; set; } = new List<AreaModel>();
}