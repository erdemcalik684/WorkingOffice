using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Repositories;

namespace UdemyNLayerProject.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        //Amaç : repositoryi i miras aldık
        // o yüzden burdada bir constructor alınması gerekiyor.
        // o yüzden buraya dbcontext tanımlaması yaptık...
        //_context , repository'den geliyor.unutma.
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public ProductRepository(AppDbContext context) : base(context)
        {
            //default constructor istiyor...
        }
        public  async Task<Product> GetWithCategoryByIdAsync(int productId)
        {
            //bir productıd vericem
            //bana bir product dönecek
            //hemde o producta bağlı olan categorileri dönecek...
            return  await  appDbContext.Products.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == productId);

        }
    }
}
