namespace MpShopping.Web.Models.Data.Dto
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
