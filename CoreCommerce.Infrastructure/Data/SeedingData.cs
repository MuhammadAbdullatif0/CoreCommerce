using CoreCommerce.Core.Entities;
using System.Text.Json;

namespace CoreCommerce.Infrastructure.Data;

public class SeedingData
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Categories.Any())
        {
            var CategoryData = File.ReadAllText("../CoreCommerce.Infrastructure/Data/Seeding/categories.json");
            var Categories = JsonSerializer.Deserialize<List<Category>>(CategoryData);
            context.Categories.AddRange(Categories);
        }

        if (!context.Products.Any())
        {
            var ProductData = File.ReadAllText("../CoreCommerce.Infrastructure/Data/Seeding/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
            context.Products.AddRange(products);
        }

        if (context.ChangeTracker.HasChanges())
            await context.SaveChangesAsync();
    }
}
