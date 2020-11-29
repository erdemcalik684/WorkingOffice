using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.API.DTOs;
using UdemyNLayerProject.Core.Services;

namespace UdemyNLayerProject.API.Filters
{
    public class NotFoundFilter : ActionFilterAttribute
    {
        // Açıklama =>  ,
        // verilen id ye göre herhangi bir product yoksa
        //daha metodun içine girmeden bu tanımladığımız filter buna dur diyecek.

        private readonly IProductService _productService;
        //private readonly IService<Product>'ta diyebilirsin.
        public NotFoundFilter(IProductService productService)
        {
            _productService = productService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id =(int)context.ActionArguments.Values.FirstOrDefault();//metotlarda tanımladığımız id leri yakalayacak.
            var product = await _productService.GetByIdAsync(id);
            if (product!=null)
            {
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Status = 404;
                errorDto.Error.Add($"Id'si {id} olan ürün veritabanında bulunamadı!");
                context.Result = new NotFoundObjectResult(errorDto);
            }


            }
    }
}
