using System.ComponentModel;

namespace Shop.Models
{
    public class EditProduct
    {
        public bool IsEditMode
        {
            get { return Id == 0; }
        }

        public int Id { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }

        [DisplayName("Категория")]
        public string Category { get; set; }

        [DisplayName("Производитель")]
        public string Vendor { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}