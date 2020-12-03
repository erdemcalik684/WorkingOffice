using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Web.APIService;
using UdemyNLayerProject.Web.DTOs;
using UdemyNLayerProject.Web.Filters;

namespace UdemyNLayerProject.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryAPIService _categoryAPIService; //httpClient üzerinden haberleşmek için bu kullanılıyor.
        
        //API Katmanıyla aynı şekilde yapacağız...
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper, CategoryAPIService categoryAPIService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _categoryAPIService = categoryAPIService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryAPIService.GetAllAsync();
            return View(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            await _categoryAPIService.AddAsync(categoryDto);
            return RedirectToAction("Index");
        }

        //update asyn üzerinden gerçekleşmiyor.
        //fakat ilk önce id yi çağıracağım için getbyıd metotdu async'dir.
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryAPIService.GetByIdAsync(id);
            return View(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
           await _categoryAPIService.Update(categoryDto);
            return RedirectToAction("Index");
        }


        //delete için yazılan metot async değil
        //fakat id yi çekeceğimiz için idyi alma metodu asyn olduğu için problem olmaması için result ile bastırıyoruz.
        
        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Delete(int id)
        {
            //var category = await _categoryAPIService.GetByIdAsync(id);
            await  _categoryAPIService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}