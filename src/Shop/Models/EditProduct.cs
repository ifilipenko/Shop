using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [Required, DisplayName("Категория")]
        public string Category { get; set; }
        [Required, DisplayName("Производитель")]
        public string Vendor { get; set; }
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}