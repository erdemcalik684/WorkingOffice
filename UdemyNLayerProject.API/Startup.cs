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
            //startup güncellemesi 3. AutoMapper Ýçin...Tabi bundan sonra
            //mapping adlý klasörün içerine dikkat et...
            services.AddAutoMapper(typeof(Startup));
            //startup güncellemesi 3.




            //startup güncellemesi 5 - NotFoundFilter için...
            services.AddScoped<NotFoundFilter>();
            //startup güncellemesi 5 - NotFoundFilter için...




            //startup güncellemesi 2.
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //startup güncellemesi 2.






            //startup güncellemesi 1.
            //appsettings.json içinde baðlantý ayarlamasýný yaptým.
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration
                    ["ConnectionStrings:SqlConStr"].ToString(),
                    o=> {
                        o.MigrationsAssembly("UdemyNLayerProject.Data");
                    });
            });
            //startup güncellemesi 1.


            services.AddControllers();




            //startup güncellemesi 4. Validation Filter için (hata kontrollerimizi bizim yönetmemiz için)
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                //açýklamasý => sen hatalarý kontrol etme yada dönme ben yaparým dedik.
            });
            //startup güncellemesi 4.

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
