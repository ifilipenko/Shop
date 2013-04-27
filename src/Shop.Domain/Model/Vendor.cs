using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.Model
{
    public class Vendor
    {
        public int Id { get; set; }
        [Required, StringLength(150)]
        public string Name { get; set; }
        [Required, StringLength(12, MinimumLength = 10)]
        public string INN { get; set; }
    }
}