using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Web.DTOs;

namespace UdemyNLayerProject.Web.APIService
{
    public class CategoryAPIService
    {
        //MVC'nin API ile haberleşebilmesi için httpclient sınıfını eklemen gerekiyor.
        private readonly HttpClient _httpClient;
        public CategoryAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        //GetAllAsync() Metodu
        //api ile haberleşmede HEP DTO sınıflarını döndürmen gerekir.
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            IEnumerable<CategoryDto> categoryDtos;
            var response = await _httpClient.GetAsync("categories"); //_httpClient sınıfı get,post,put ve delete metotlarını barındırır.
            //response'dan bize json data geldi şimdi.
            //daha sonra bu gelen json datayı categoryDtos'a cast yani dönüştürme yapman gerekiyor.
            //onu da başarılı olduktan sonra yapman gerekiyor.
            //bunuda newtonsoft kütüphanesi üzerinden yapman gerekiyor.
            if (response.IsSuccessStatusCode) //APİLERİN BİZE GÖNDERDİĞİ BAŞARILI DURUM KODUDUR.
            {
                categoryDtos = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(await response.Content.ReadAsStringAsync());

            }
            else
            {
                categoryDtos = null;
            }
            return categoryDtos;
        }



        //AddAsync() Metodu
        public async Task<CategoryDto> AddAsync(CategoryDto categoryDto)
        {
            //postasync url ve content istiyor.

            var stringContent = new StringContent(JsonConvert.SerializeObject(categoryDto),Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("categories", stringContent);


            if (response.IsSuccessStatusCode)
            {
                categoryDto = JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
                return categoryDto;
            }
            else
            {
                return null;
            }
        }


        //update() metodu.
        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"categories/{id}"); // $ ile string ifadenin içerisinde değişken tanımlayabiliyoruz.
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }

        //update ile beraber geriye birşey dönmemek uygundur.
        public async Task<bool> Update(CategoryDto categoryDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(categoryDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("categories", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"categories/{id}"); // $ ile string ifadenin içerisinde değişken tanımlayabiliyoruz.
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
