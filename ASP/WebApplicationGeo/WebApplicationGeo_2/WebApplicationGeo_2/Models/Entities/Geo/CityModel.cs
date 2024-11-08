namespace WebApplicationGeo_2.Models.Entities.Geo;
using System.ComponentModel.DataAnnotations.Schema;

public class CityModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int AreaId { get; set; }
    [ForeignKey("AreaId")]
    public AreaModel? Area { get; set; }
}