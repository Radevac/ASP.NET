using System.ComponentModel.DataAnnotations;

namespace MyFirstWebServer.Models.Entities
{
    public class MyContact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}