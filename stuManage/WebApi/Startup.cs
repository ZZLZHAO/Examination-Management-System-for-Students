using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Service;
using SqlSugar.IOC;
using System;
using System.Text;


namespace WebApi
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
            services.AddCustomIOC();
            services.AddCustomJWT();

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
        
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    static class IOCExtend
    {
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {
            services.AddScoped<AnnounceRepository>();
            services.AddScoped<AnnounceService>();
            services.AddScoped<CustomerRepository>();
            services.AddScoped<CustomerService>();
            services.AddScoped<ExamInfoRepository>();
            services.AddScoped<ExamInfoService>();
            services.AddScoped<ScoreRepository>();
            services.AddScoped<ScoreService>();
            services.AddScoped<StuExamRepository>();
            services.AddScoped<StuExamService>();
            services.AddScoped<StudentRepository>();
            services.AddScoped<StudentService>();
            services.AddScoped<CompInfoRepository>();
            services.AddScoped<CompInfoService>();
            services.AddScoped<StuCompRepository>();
            services.AddScoped<StuCompService>();
            services.AddScoped<QuestionRepository>();
            services.AddScoped<QuestionService>();
            services.AddScoped<AnswerRepository>();
            services.AddScoped<AnswerService>();
            services.AddScoped<ReplyRepository>();
            services.AddScoped<ReplyService>();
            services.AddScoped<ProblemRepository>();
            services.AddScoped<ProblemService>();
            services.AddScoped<TeacherRepository>();
            services.AddScoped<TeacherService>();


            return services;
        }
        public static IServiceCollection AddCustomJWT(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GBTR-OEUR1-DCS-UYTGR-SDFGTRE-ES")),
                        ValidateIssuer = true,
                        ValidIssuer = MySetting.Mysetting.issuerURL,
                        ValidateAudience = true,
                        ValidAudience = MySetting.Mysetting.audienceURL,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(60)
                    };
                });
            return services;
        }

    }
}
