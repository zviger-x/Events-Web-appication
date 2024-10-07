using EventsManagement.BusinessLogic.AutoMapping;
using EventsManagement.DataAccess.Contexts;
using EventsManagement.DataObjects.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace EventsManagement.WebAPI.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            JwtSettings.Set(configuration["Jwt:Key"], configuration["Jwt:Issuer"], configuration["Jwt:Audience"]);

            var connectionString = configuration.GetConnectionString("DB");
            services.AddDbContext<EventsManagementDbContext>(options => options.UseSqlServer(connectionString));
            ScopesConfigurator.AddScopes(services);

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<EventUserCounterMappingAction>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Events management API", Version = "v1" });

                // Определение для использования Bearer токена
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = $"Введите 'Bearer [token]'. Должно выглядеть так 'Bearer eyJhbGci...'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                // Применение определения безопасности ко всем API
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            services.AddCors(c => c.AddPolicy("CorsPolicy", policyBuilder =>
            {
                policyBuilder.AllowAnyHeader();
                policyBuilder.AllowAnyMethod();
                policyBuilder.WithOrigins(configuration["Links:Client"]);
            }));

            services.AddIdentityServer()
                .AddInMemoryClients(IdentityServerConfig.GetClients(configuration))
                .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes(configuration))
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources(configuration))
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources(configuration))
                .AddDeveloperSigningCredential();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = JwtSettings.Issuer,
                        ValidAudience = JwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SecretKey))
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseIdentityServer();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
