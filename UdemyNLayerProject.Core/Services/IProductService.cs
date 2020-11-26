using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Core.Services
{
    public interface IProductService : IService<Product>
    {
        /****************Açıklama********************////
        //buraya yazılacak kodun amacı : 
        // şimdi repositories katmanı sadece veritabanı işlemlerini halledecek metotlardan oluşacağı için bu katmanda ayrı bir metotlar tanımlayabilirsin.

        //Barkodun doğru olup olmadığını kontrol etmek için yazılan metot.
        //Database ile hiçbir ilişkisi yok.
        //İç taraftaki API barkod ile burdaki barkodu karşılaştırmak için yazdım örnek olarak...
        //Product hesaplamalarını buraya yazabilirsin...

        //bool ControlInnerBarcode(Product product);




        //Amaç : ıd ye göre product nesnesini alalım ama o producta bağlı olan categoryi de alalım istiyoruz.
        Task<Product> GetWithCategoryByIdAsync(int productId);

    }
}
