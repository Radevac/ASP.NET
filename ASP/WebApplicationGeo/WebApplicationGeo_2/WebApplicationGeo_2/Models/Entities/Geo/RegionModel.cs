namespace WebApplicationGeo_2.Models.Entities.Geo;
using System.ComponentModel.DataAnnotations.Schema;

public class RegionModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public List<AreaModel> Areas { get; set; } =  new List<AreaModel>();
}