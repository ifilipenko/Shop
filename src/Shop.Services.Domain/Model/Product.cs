using System.ComponentModel.DataAnnotations;

namespace Shop.Services.Domain.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Vendor { get; set; }
        public string Description { get; set; }
    }
}