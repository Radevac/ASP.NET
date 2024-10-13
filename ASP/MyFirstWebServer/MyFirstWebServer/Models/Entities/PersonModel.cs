using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstWebServer.Models.Entities;

public class PersonModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int Age { get; set; }
    
    public string Gender { get; set; }
}