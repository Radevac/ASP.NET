namespace MyFirstWebServer.Models.Entities;

public class HouseModel
{
    public int Id { get; set; }
    public string Number { get; set; }
    
    public string Slug { get; set; }

    // Foreign Key to Street
    public int StreetId { get; set; }
    public StreetModel Street { get; set; }
}