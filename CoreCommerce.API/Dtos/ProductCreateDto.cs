namespace CoreCommerce.API.Dtos;

public class ProductCreateDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public IFormFile Img { get; set; }
    public int ProductCategoryId { get; set; }
}
