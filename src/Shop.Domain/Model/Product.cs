namespace Shop.Domain.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Category Category { get; set; }
        public virtual Vendor Vendor { get; set; }
        public string Description { get; set; }
    }
}