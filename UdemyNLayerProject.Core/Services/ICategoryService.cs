using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Core.Services
{
    public interface ICategoryService:IService<Category>
    {
        Task<Category> GetWithProductsByIdAsync(int categoryId);

        //Categorye özgü olan metotlarımız var ise database ile ilişkili olmayan buraya tanımlayabiliriz.
        // hesaplama ile ilgili metotlar olabilir.
        //özel bir url oluşturabiliriz gibi gibi...
    }
}
