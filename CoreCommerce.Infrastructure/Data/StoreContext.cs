using CoreCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace CoreCommerce.Infrastructure.Data;

public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType.GetProperty("IsDeleted") != null)
            {
                var parameter = Expression.Parameter(entityType.ClrType);
                var filterExpression = Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, "IsDeleted"),
                        Expression.Constant(false)
                    ),
                    parameter
                );
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filterExpression);
            }
        }
    }
}
