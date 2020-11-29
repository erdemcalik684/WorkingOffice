using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id); // primary key olsun.
            builder.Property(x => x.Id).UseIdentityColumn(); // birer birer artsın.

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.SurName).IsRequired().HasMaxLength(100);


        }
    }
}
