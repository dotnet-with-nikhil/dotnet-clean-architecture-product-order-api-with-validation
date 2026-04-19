namespace dotnet_example_clean_arch_with_entity_framework.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public Products Product { get; set; }
    }
}
