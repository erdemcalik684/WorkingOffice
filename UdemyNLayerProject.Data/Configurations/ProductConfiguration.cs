using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id); // primary key olsun.
            builder.Property(x => x.Id).UseIdentityColumn(); // birer birer artsın.

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired();

            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)"); // toplam 18 hane olsun ve virgülden sonra 2 değer alabilecek demektir.Para değeri olduğu için...

            builder.Property(x => x.InnerBarcode).HasMaxLength(50);

            //IsDeleted bool olduğundan dolayı default olarak false olacaktır.


            builder.ToTable("Products");


        }
    }
}
