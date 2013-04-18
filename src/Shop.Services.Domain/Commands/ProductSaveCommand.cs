namespace Shop.Services.Domain.Commands
{
    public class ProductSaveCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
    }
}