namespace MyFirstWebServer.Models.Entities;

public class CityTypeModel
{
    public int Id { get; set; }
    public string TypeName { get; set; }
    
    public string Slug { get; set; }

    // Many-to-many relationship with City
    public ICollection<CityModel> Cities { get; set; }
}