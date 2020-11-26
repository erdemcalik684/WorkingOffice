using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        //Amaç : ıd ye göre product nesnesini alalım ama o producta bağlı olan categoryi de alalım istiyoruz.
        Task<Product> GetWithCategoryByIdAsync(int productId);
    }
}
