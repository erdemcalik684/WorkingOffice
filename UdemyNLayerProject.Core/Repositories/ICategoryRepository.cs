using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        //Toplu metotlar implement ettik.
        //Ayrıca; Kategoriye özgü bir metotlar buraya yazılır.
        // Örnek Metot : Belirlenen kategori ıd sine göre , o kategori dönsün ve o kategoriye ait ürünler dizi şeklinde dönsün.
        // Yani categoriye bağlı diğer entitylerinde (product) dönmesini bu şekilde sağlıyoruz.
        Task<Category> GetWithProductsByIdAsync(int categoryId);

    }
}
