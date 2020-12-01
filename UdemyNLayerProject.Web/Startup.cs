using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UdemyNLayerProject.Core.Repositories;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Core.UnitOfWorks;
using UdemyNLayerProject.Data;
using UdemyNLayerProject.Data.Repositories;
using UdemyNLayerProject.Data.UnitOfWorks;
using UdemyNLayerProject.Service.Services;
using UdemyNLayerProject.Web.Filters;

namespace UdemyNLayerProject.Web
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
            //NotFoundFilter i�in bu eklenmeli.��nk� dependency injection.
            services.AddScoped<NotFoundFilter>();



            /*AutoMapper Aktif Olmas� i�in nugetten y�kledikten sonra bu service eklenir...*/
            //startup g�ncellemesi 3. 
            //mapping adl� klas�r�n i�erine dikkat et...
            services.AddAutoMapper(typeof(Startup));
            //startup g�ncellemesi 3.

            /************              AddScoped Ne ��e Yarar ?             *******************/
            //bir request i�erisinde bir interfacele kar��la��rsa,
            //gidiyordu ona kar�� gelen classtan bir nesne �rne�i olu�turuyor.
            //startup g�ncellemesi 2 a��l��.
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));//genel generic
            services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));//genel generic
            services.AddScoped<ICategoryService, CategoryService>();//�zel generic 
            services.AddScoped<IProductService, ProductService>(); // �zel generic
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //UnitofWork ne ��e Yarar
            /*�rne�in 5 tablomuz olsun bu tablolar�m�z birbiriyle ili�kili olabilir ve bizim seneryomuzda bunlar�n hepsine ayn� anda g�ncelleme ekleme yap�labilir*/
            /*Bu seneryoda biz kendimiz bu i�lemleri yaparsak bir tabloya veri ekleyip di�erine eklemeyi unutabiliriz.*/
            /*Bu i�lemi biz yapmak yerine UnitofWork sen b�t�n talimatlar� bana ver ben hepsini yapar�m diyor.*/
            //startup g�ncellemesi 2 kapan��.

            //startup g�ncellemesi 1 a��l��.
            //appsettings.json i�inde ba�lant� ayarlamas�n� yapt�m.
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration
                    ["ConnectionStrings:SqlConStr"].ToString(),
                    o => {
                        o.MigrationsAssembly("UdemyNLayerProject.Data");
                    });
            });



            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
