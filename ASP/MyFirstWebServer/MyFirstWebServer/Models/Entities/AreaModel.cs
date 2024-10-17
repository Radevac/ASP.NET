namespace MyFirstWebServer.Models.Entities;

public class AreaModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public string Slug { get; set; }

    public ICollection<CityModel> Cities { get; set; }
}