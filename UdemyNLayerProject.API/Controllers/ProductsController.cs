using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyNLayerProject.API.DTOs;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;

namespace UdemyNLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //API projem hep service katmanıyla haberleşecek.
        //neden core katmanından aldım(IProductService)...
        //çünkü startup da IProductService'i görünce, Service katmanındaki ProductService si getirecek...

        public readonly IProductService _productService;
        public readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var products = await _productService.GetByIdAsync(id);
            return Ok(_mapper.Map<ProductDto>(products));
        }



        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var newProduct = await _productService.AddAsync(_mapper.Map<Product>(productDto));
            return Created(string.Empty, _mapper.Map<ProductDto>(newProduct));
        }

        [HttpPut]
        public IActionResult Update(ProductDto productDto)
        {
            var product = _productService.Update(_mapper.Map<Product>(productDto));
            return NoContent();
        }



        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var product = _productService.GetByIdAsync(id).Result;//async keywordu kullnamak istemiyorsak result diyebiliriz.
            _productService.Remove(product);
            return NoContent();
        }

        //api/product/1/category postman üzerinde bu şekilde istek gönderilebilir.
        //id si 1 olan ürün gelir. yanında ait olduğu kategori bilgisi de gelir.
        [HttpGet("{id}/category")]
        public async Task<IActionResult> GetWithCategoryById(int id)
        {
            var product = await _productService.GetWithCategoryByIdAsync(id);
            return Ok(_mapper.Map<ProductWithCategoryDto>(product));
        }





      






    }
}
