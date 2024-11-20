using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationGeo.Models.Cars.Toyota;

public class ConfigurationColorsModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [DisplayName("Main Image for Car in This Color")]
    public string MainImageUrl { get; set; }
    
    public int ColorId { get; set; }
    
    [ForeignKey("ColorId")]
    public ColorModel Color { get; set; }
    
    public int ConfigurationId { get; set; }
    
    [ForeignKey("ConfigurationId")]
    public ConfigurationModel Configuration { get; set; }
}