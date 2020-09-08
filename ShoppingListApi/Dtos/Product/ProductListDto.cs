namespace ShoppingListApi.Dtos.Product
{
    public class ProductListDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}