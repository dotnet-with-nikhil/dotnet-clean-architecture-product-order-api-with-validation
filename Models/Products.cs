namespace dotnet_example_clean_arch_with_entity_framework.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
