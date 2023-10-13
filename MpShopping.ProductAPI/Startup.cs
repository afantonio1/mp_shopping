using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MpShopping.ProductAPI.Config;
using MpShopping.Web.Models.Interfaces;
using MpShopping.ProductAPI.Model.Context;
using MpShopping.ProductAPI.Model.Repositories;

namespace MpShopping.ProductAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration["MySQlConnection:MySQlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options
                    .UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 32))));

            //AutoMapper
            IMapper mapper = MappingConfig.RegisterMap().CreateMapper(); ;
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Repositores
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
