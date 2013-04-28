using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Shop.Domain.Model;

namespace Shop.Models
{
    public class EditProduct
    {
        public bool IsEdit
        {
            get { return Id != 0; }
        }

        public int Id { get; set; }
        [Required, MinLength(3), DisplayName("Наименование")]
        public string Name { get; set; }
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required, DisplayName("Категория")]
        public int? CategoryId { get; set; }
        [Required, DisplayName("Производитель")]
        public int? VendorId { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Vendor> Vendors { get; set; }
    }
}