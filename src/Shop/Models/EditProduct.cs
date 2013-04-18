using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class EditProduct
    {
        public bool IsEditMode
        {
            get { return Id != 0; }
        }

        public int Id { get; set; }

        [DisplayName("Наименование"), Required]
        public string Name { get; set; }

        [DisplayName("Категория"), Required]
        public string Category { get; set; }

        [DisplayName("Производитель"), Required]
        public string Vendor { get; set; }

        [DisplayName("Описание"), Required]
        public string Description { get; set; }
    }
}