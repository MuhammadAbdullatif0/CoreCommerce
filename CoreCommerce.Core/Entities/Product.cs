namespace CoreCommerce.Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImgUrl { get; set; }
    public int ProductCategoryId { get; set; }
    public Category ProductCategory { get; set; }
}
