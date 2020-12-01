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
            //NotFoundFilter için bu eklenmeli.Çünkü dependency injection.
            services.AddScoped<NotFoundFilter>();



            /*AutoMapper Aktif Olmasý için nugetten yükledikten sonra bu service eklenir...*/
            //startup güncellemesi 3. 
            //mapping adlý klasörün içerine dikkat et...
            services.AddAutoMapper(typeof(Startup));
            //startup güncellemesi 3.

            /************              AddScoped Ne Ýþe Yarar ?             *******************/
            //bir request içerisinde bir interfacele karþýlaþýrsa,
            //gidiyordu ona karþý gelen classtan bir nesne örneði oluþturuyor.
            //startup güncellemesi 2 açýlýþ.
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));//genel generic
            services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));//genel generic
            services.AddScoped<ICategoryService, CategoryService>();//özel generic 
            services.AddScoped<IProductService, ProductService>(); // özel generic
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //UnitofWork ne Ýþe Yarar
            /*Örneðin 5 tablomuz olsun bu tablolarýmýz birbiriyle iliþkili olabilir ve bizim seneryomuzda bunlarýn hepsine ayný anda güncelleme ekleme yapýlabilir*/
            /*Bu seneryoda biz kendimiz bu iþlemleri yaparsak bir tabloya veri ekleyip diðerine eklemeyi unutabiliriz.*/
            /*Bu iþlemi biz yapmak yerine UnitofWork sen bütün talimatlarý bana ver ben hepsini yaparým diyor.*/
            //startup güncellemesi 2 kapanýþ.

            //startup güncellemesi 1 açýlýþ.
            //appsettings.json içinde baðlantý ayarlamasýný yaptým.
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
