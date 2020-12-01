using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UdemyNLayerProject.API.Filters;
using UdemyNLayerProject.Core.Repositories;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Core.UnitOfWorks;
using UdemyNLayerProject.Data;
using UdemyNLayerProject.Data.Repositories;
using UdemyNLayerProject.Data.UnitOfWorks;
using UdemyNLayerProject.Service.Services;

namespace UdemyNLayerProject.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //startup g�ncellemesi 3. AutoMapper ��in...Tabi bundan sonra
            //mapping adl� klas�r�n i�erine dikkat et...
            services.AddAutoMapper(typeof(Startup));
            //startup g�ncellemesi 3.




            //startup g�ncellemesi 5 - NotFoundFilter i�in...
            services.AddScoped<NotFoundFilter>();
            //startup g�ncellemesi 5 - NotFoundFilter i�in...



            /************              AddScoped Ne ��e Yarar ?             *******************/
            //bir request i�erisinde bir interfacele kar��la��rsa,
            //gidiyordu ona kar�� gelen classtan bir nesne �rne�i olu�turuyor.
            //startup g�ncellemesi 2 a��l��.
            //startup g�ncellemesi 2.
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //UnitofWork ne ��e Yarar
            /*�rne�in 5 tablomuz olsun bu tablolar�m�z birbiriyle ili�kili olabilir ve bizim seneryomuzda bunlar�n hepsine ayn� anda g�ncelleme ekleme yap�labilir*/
            /*Bu seneryoda biz kendimiz bu i�lemleri yaparsak bir tabloya veri ekleyip di�erine eklemeyi unutabiliriz.*/
            /*Bu i�lemi biz yapmak yerine UnitofWork sen b�t�n talimatlar� bana ver ben hepsini yapar�m diyor.*/
            //startup g�ncellemesi 2 kapan��.

            //startup g�ncellemesi 1 a��l��.
            //appsettings.json i�inde ba�lant� ayarlamas�n� yapt�m.
            //startup g�ncellemesi 2.






            //startup g�ncellemesi 1.
            //appsettings.json i�inde ba�lant� ayarlamas�n� yapt�m.
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration
                    ["ConnectionStrings:SqlConStr"].ToString(),
                    o=> {
                        o.MigrationsAssembly("UdemyNLayerProject.Data");
                    });
            });
            //startup g�ncellemesi 1.


            services.AddControllers();




            //startup g�ncellemesi 4. Validation Filter i�in (hata kontrollerimizi bizim y�netmemiz i�in)
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                //a��klamas� => sen hatalar� kontrol etme yada d�nme ben yapar�m dedik.
            });
            //startup g�ncellemesi 4.

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
