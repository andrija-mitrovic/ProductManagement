using Microsoft.EntityFrameworkCore;
using ProductManagement.Categories;
using ProductManagement.Products;
using Volo.Abp;

namespace ProductManagement.EntityFrameworkCore
{
    public static class ProductManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureProductManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            ////Configure your own tables/entities 
            //builder.Entity<Book>(b =>
            //{
            //    b.ToTable(BookStoreConsts.DbTablePrefix + "Books", BookStoreConsts.DbSchema);
            //    b.ConfigureByConvention(); // auto configure for the base class props
            //    b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            //});

            ConfigureCategoriesTable(builder);
            ConfigureProductsTable(builder);
        }

        private static void ConfigureCategoriesTable(ModelBuilder builder)
        {
            builder.Entity<Category>(b =>
            {
                b.ToTable("Categories");
                b.Property(x => x.Name)
                    .HasMaxLength(CategoryConsts.MaxNameLength)
                    .IsRequired();
                b.HasIndex(x => x.Name);
            });
        }

        private static void ConfigureProductsTable(ModelBuilder builder)
        {
            builder.Entity<Product>(b =>
            {
                b.ToTable("Products");
                b.Property(x => x.Name)
                    .HasMaxLength(ProductConsts.MaxNameLength)
                    .IsRequired();
                b.HasOne(x => x.Category)
                    .WithMany()
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                b.HasIndex(x => x.Name).IsUnique();
            });
        }
    }
}
