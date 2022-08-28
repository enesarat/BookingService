using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using BookingService.DataAccess;
using BookingService.Business.Abstract;
using BookingService.Business.Concrete;
using BookingService.DataAccess.EntityFramework;
using BookingService.DataAccess.Abstract;

namespace BookingService.API
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
            services.AddRazorPages();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            /*services.AddDbContext<BookingServiceDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("BookingServiceDbContext")));
            */

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddControllers();

            services.AddSingleton<IBookingsService, BookingManager>();
            services.AddSingleton<IBookingsDAL,EfBookingsRepository>();

            services.AddSingleton<IAppartmentsService, AppartmentsManager>();
            services.AddSingleton<IAppartmentsDAL, EfAppartmentsRepository>();

            services.AddSingleton<ICompanyService, CompanyManager>();
            services.AddSingleton<ICompanyDAL, EfCompanyRepository>();

            services.AddSingleton<IUsersService, UsersManager>();
            services.AddSingleton<IUsersDAL, EfUsersRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options=>options.AllowCredentials().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
