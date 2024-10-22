namespace CoreCommerce.API.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImgUrl { get; set; }
    public string ProductCategory { get; set; }
}
