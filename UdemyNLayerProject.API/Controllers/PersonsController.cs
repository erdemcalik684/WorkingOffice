using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;

namespace UdemyNLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        //Burada product ve categoriden farklı olarak birşey yaptım
        //Direkt IService üzerinden iletişm kurdum.
        //ve ilk metotda fark edersen(getall) mapper dto larla oluşturmama gerek kalmadı...
        private readonly IService<Person> _personService;
        public PersonsController(IService<Person> personService)
        {
            _personService = personService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _personService.GetAllAsync();
            return Ok(persons);
        }

        //farkedersen dto almadan da yapılabiliyormuş.
        [HttpPost]
        public async Task<IActionResult> Save(Person person)
        {
            var newPerson = await _personService.AddAsync(person);
            return Ok(newPerson);

        }
    }
}
