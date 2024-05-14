using AspNetCore.IServiceCollection.AddIUrlHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using School.Data.Entities;
using School.Infrastructure;
using School.Service;
using SchoolWepApi.MiddleWares;
using System.Globalization;
using System.Text;
namespace SchoolWepApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddUrlHelper();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructureDependcies()
                            .AddServiceDependcies()
                            .AddCoreDependcies();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["jwtSettings:validateIssuer"],
                    ValidAudience = builder.Configuration["jwtSettings:validateAudience"],
                    IssuerSigningKey =
                     new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["jwtSettings:secretkey"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization(option =>
            {
                option.AddPolicy("Create", policy =>
                {
                    policy.RequireClaim("Create", "True");
                });
            });

            #region Localization

            builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);


            builder.Services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "";
            });

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en-US"),
            new CultureInfo("ar-EG"),
        };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            #endregion

            builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
            {
                options.UseLazyLoadingProxies().
                // UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).
                UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;

            }).AddEntityFrameworkStores<ApplicationDatabaseContext>().AddDefaultTokenProviders();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<LimitNumberOfRequestsMiddleWare>();
            app.MapControllers();

            var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.Run();
        }
    }
}
