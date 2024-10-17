namespace MyFirstWebServer.Models.Entities;

public class CityModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public string Slug { get; set; }

    // Foreign Key to Area
    public int AreaId { get; set; }
    public AreaModel Area { get; set; }

    public ICollection<StreetModel> Streets { get; set; }
    public ICollection<CityTypeModel> CityTypes { get; set; }
}