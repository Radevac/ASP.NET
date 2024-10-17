namespace MyFirstWebServer.Models.Entities;

public class StreetModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public string Slug { get; set; }

    // Foreign Key to City
    public int CityId { get; set; }
    public CityModel City { get; set; }

    public ICollection<HouseModel> Houses { get; set; }
}