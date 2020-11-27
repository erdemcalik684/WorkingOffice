using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Data.Configurations;
using UdemyNLayerProject.Data.Seeds;

namespace UdemyNLayerProject.Data
{
    public class AppDbContext : DbContext
    {
        //1.aşama
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
        }

        //2.aşama
        //Tabloları ekle.Yani classları tablo haline çeviriyoruz...
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //3.aşama
        //vt ve tablolar oluşmadan önce oluşacak metot.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //product ve category tablolarında hangi proplar pk veya fk olacak
            //hangi alanlar ne şekilde tanımlanacak buraya yazılabilir...
            //tabi buraya yazabiliriz ama class içinde yapacağım.
            //classımda Data/Configuration/Product yada Category Configuration içinde yazdım.


            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            //IEntityTypeConfiguration kullanacaksın kısacası...


            //Seed data için Data/Seed/ klasörünün içerisine  kod yazdık.Bu kodu buraya entegre etmemiz gerekiyor.
            modelBuilder.ApplyConfiguration(new ProductSeed(new int[] {1,2}));
            modelBuilder.ApplyConfiguration(new CategorySeed(new int[] {1,2}));
           
        }
    }
}
