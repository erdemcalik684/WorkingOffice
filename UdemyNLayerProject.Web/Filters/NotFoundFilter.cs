using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.Web.APIService;
using UdemyNLayerProject.Web.DTOs;

namespace UdemyNLayerProject.Web.Filters
{
    public class NotFoundFilter : ActionFilterAttribute
    {
        // Açıklama =>  ,
        // verilen id ye göre herhangi bir product yoksa
        //daha metodun içine girmeden bu tanımladığımız filter buna dur diyecek.

        private readonly CategoryAPIService _categoryAPIService;
        //private readonly IService<Product>'ta diyebilirsin.
        public NotFoundFilter(CategoryAPIService categoryAPIService)
        {
            _categoryAPIService = categoryAPIService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();//metotlarda tanımladığımız id leri yakalayacak.
            var product = await _categoryAPIService.GetByIdAsync(id);
            if (product != null)
            {
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();
                //api den farklı olarak json döndürmeyeceğiz
                errorDto.Error.Add($"Id'si {id} olan kategori veritabanında bulunamadı!");
                context.Result = new RedirectToActionResult("Error", "Home", errorDto);
                //object dönmek yerine sayfa yönlendiricez bu mvc side
                //error actionu, home controller sayfasındaki , errordtosunuda göster diyoruz.
            }


        }
    }
}
