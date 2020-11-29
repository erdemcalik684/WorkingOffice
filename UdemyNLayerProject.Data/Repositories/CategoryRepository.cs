using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Repositories;

namespace UdemyNLayerProject.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        //Amaç : repositoryi i miras aldık
        // o yüzden burdada bir constructor alınması gerekiyor.
        // o yüzden buraya dbcontext tanımlaması yaptık...
        //_context , repository'den geliyor.unutma.
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public CategoryRepository(AppDbContext context) : base(context)
        {
            //default constructor istiyor...
        }
        public async Task<Category> GetWithProductsByIdAsync(int categoryId)
        {
            //bir category id vericem
            //bana o categoriyi dönücek
            //hemde o categoriye bağlı olan ürünleri dönecek.
            return await appDbContext.Categories.Include(x => x.Products)
                .SingleOrDefaultAsync(x => x.Id == categoryId);
        }
    }
}
