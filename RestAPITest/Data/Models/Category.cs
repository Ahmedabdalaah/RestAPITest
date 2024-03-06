using System.ComponentModel.DataAnnotations;

namespace RestAPITest.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Notes { get; set; }

        public List<Item> Items { get; set; }

    }
}
