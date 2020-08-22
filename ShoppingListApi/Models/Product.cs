namespace ShoppingListApi.Models
{
    public class Product: Entity
    {
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}