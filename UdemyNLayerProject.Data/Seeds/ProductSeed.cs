using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Data.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        private readonly int[] _ids;
        public ProductSeed(int[] ids)
        {
            _ids = ids;
        }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, Name = "Pilot Kalem", Price = 12.59m, Stock = 100, CategoryId = _ids[0] },
                new Product { Id = 2, Name = "Kurşun Kalem", Price = 12.59m, Stock = 200, CategoryId = _ids[0] },
                new Product { Id = 3, Name = "Boya Kalemi", Price = 13.44m, Stock = 410, CategoryId = _ids[1] },
                new Product { Id = 4, Name = "Tükenmez Kalem", Price = 9.51m, Stock = 240, CategoryId = _ids[1] },
                new Product { Id = 5, Name = "Kırmızı Kalem", Price = 24.00m, Stock = 200, CategoryId = _ids[1] }
                );
        }
    }
}
