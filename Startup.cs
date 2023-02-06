using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumm_Back_End.Configuration;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models;
using Yumm_Back_End.Repository;
using Yumm_Back_End.Repository._AddressRepository.ProvinceRepository;
using Yumm_Back_End.Repository._Auth;
using Yumm_Back_End.Repository._Restaurant;
using AutoMapper;
using Yumm_Back_End.Mapper;
using Yumm_Back_End.Repository._AddressRepository.CityRepository;
using Yumm_Back_End.Repository._AddressRepository.CommuneRepository;
using Yumm_Back_End.Repository._AddressRepository.StreetRepository;
using Yumm_Back_End.Repository._AddressRepository.NeighbourhoodRepository;
using Yumm_Back_End.Repository._AddressRepository.LocationRepository;
using Yumm_Back_End.Repository._ProductCategory;
using Yumm_Back_End.Repository._MenuRepository;
using Yumm_Back_End.Services;
using Yumm_Back_End.Models.User;
using Yumm_Back_End.Repository._ClientRepository;
using Yumm_Back_End.Repository._AddressRepository.UserLocationRepository;
using Yumm_Back_End.Repository._UserRepository;
using Yumm_Back_End.Repository._MenuRepository._ProductRepository;
using Yumm_Back_End.Repository._MenuRepository._MenuGroupRepository;
using Yumm_Back_End.Services.Logger;
using Yumm_Back_End.Repository._SupportTeamRepository;
using Yumm_Back_End.Services.PaginationService;
using Microsoft.AspNetCore.Http;
using Yumm_Back_End.Repository._MediaRepository;
using Yumm_Back_End.Services.MediaService;
using Yumm_Back_End.Repository._DriverRepository;
using Yumm_Back_End.Repository._OrderRepository;
using Yumm_Back_End.Repository._AddressRepository._ClientAdressRepository;

namespace Yumm_Back_End
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
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SIT_ENV"));
            });


            //
            #region Service Injection
            services.AddScoped<IAuthRepository, AuthenticationRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICommuneRepository, CommuneRepository>();
            services.AddScoped<IStreetRepository, StreetRepository>();
            services.AddScoped<INeighbourhoodRepository, NeighbourhoodRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IRestaurantTypeRepository, RestaurantTypeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();   
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IClientRepository,ClientRepository>();
            services.AddScoped<IUserLocationRepository,UserLocationRepository>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuGroupRepository, MenuGroupRepository>();
            // services.AddScoped<IMediaRepository, MediaRepository>();
            services.AddScoped<IDriverRepository,DriverRepository>();
            services.AddScoped<IProductExtrasRepository, ProductExtrasRepository>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<ISupportTeamRepository, SupportTeamRepository>();
            services.AddScoped<IOrderRepository,OrderRepository> ();
            services.AddScoped<IClientAddressRepository,ClientAddressRepository> ();
            services.AddScoped<IMediaManager, MediaManager>();
            services.AddSingleton<IUriService>(o => 
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });


            services.AddScoped<IDriverRepository, DriverRepository>();
            #endregion

            services.AddAutoMapper(typeof(YummMapping));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;

            })
              .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();



            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Yumm_Back_End", Version = "v1" });
            });

            var appSettingsSection = Configuration.GetSection("AppSettings");

            //configure the services

            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);


            //Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options => 
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yumm_Back_End v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
