using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Service;
using SqlSugar.IOC;


namespace JWT
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
            services.AddCors();
            services.AddControllers();
                        services.AddSqlSugar(new IocConfig()
            {
                ConnectionString = MySetting.Mysetting.conStr,
                DbType = IocDbType.Oracle,
                IsAutoCloseConnection = true
            });

            services.AddScoped<CustomerRepository>();
            services.AddScoped<CustomerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(
                options => options.WithOrigins(MySetting.Mysetting.conStr, "http://localhost:8080", "http://localhost:8081", "http://localhost:48735", "http://119.3.109.50:8080").AllowAnyMethod().AllowAnyHeader()
            );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
