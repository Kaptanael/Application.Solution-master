using Application.Service;
using Application.Data.DataContext;
using Application.Data.Repository;
using Application.Data.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Service.Mapper;

namespace Application.Api
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //services.AddTransient<Seed>();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient<IValueService, ValueService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<Startup>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                logger.LogInformation("Development environment");
            }
            else
            {
                logger.LogInformation($"Environment: {env.EnvironmentName}");               
                app.UseHsts();
            }

            //seeder.SeedUsers();

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
