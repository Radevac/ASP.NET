namespace Dolly.Models.Dolly;

public class DollModel
{
    public int Id { get; set; }
    public string Name { get; set; }
        
    // URL зображення для ляльки
    public string ImageUrl { get; set; }

    // Колекція одягу для ляльки
    public List<OutfitModel> Outfits { get; set; } = new List<OutfitModel>();
}