using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationGeo.Models.Cars.Toyota;

public class ToyotaModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public List<ConfigurationModel> Configurations { get; set; } = new();
}