namespace Dolly.Models.Dolly;

public class OutfitModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Style { get; set; }
        
    // URL зображення для одягу
    public string ImageUrl { get; set; }
        
    // Властивість для зв’язку з лялькою
    public int DollId { get; set; }
    public DollModel Doll { get; set; }
}