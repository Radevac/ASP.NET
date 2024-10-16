using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Entities;

public class PostModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Body { get; set; }
    
    public string Slug { get; set; }
    
    public int CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    
    public CategoryModel Category { get; set; }

    public List<TagModel> Tags { get; set; } = new();
}