using EventsManagement.BusinessLogic;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.EventService;
using EventsManagement.BusinessLogic.Services.EventUserService;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Services.UserService;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.Contexts;
using EventsManagement.DataAccess.Repositories;
using EventsManagement.DataAccess.Repositories.Interfaces;
using EventsManagement.DataObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.WebAPI.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            // Add services to the container.
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            services.AddDbContext<EventsManagementDbContext>(options => options.UseSqlServer(connectionString));
            services.AddAutoMapper(typeof(MappingProfile));
            ScopesConfigurator.AddScopes(services);

            services.AddControllers();
            services.AddEndpointsApiExplorer(); // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddSwaggerGen(c =>
            {
                // c.ResolveConflictingActions(apiDesc => apiDesc.First());
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }

    internal static class ScopesConfigurator
    {
        public static void AddScopes(IServiceCollection services)
        {
            AddValidatorsScopes(services);
            AddUnitOfWorkScopes(services);
            AddRepositoriesScopes(services);
            AddServicesScopes(services);
        }

        public static void AddRepositoriesScopes(IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventUserRepository, EventUserRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
        }

        public static void AddServicesScopes(IServiceCollection services)
        {
            AddEventScopes(services);
            AddEventUserScopes(services);
            AddUserScopes(services);
        }

        public static void AddUnitOfWorkScopes(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddValidatorsScopes(IServiceCollection services)
        {
            services.AddScoped<IBaseValidator<EventDTO>, EventValidator>();
            services.AddScoped<IBaseValidator<EventUserDTO>, EventUserValidator>();
            services.AddScoped<IBaseValidator<UserDTO>, UserValidator>();
        }

        private static void AddEventScopes(IServiceCollection services)
        {
            services.AddScoped<ICreateUseCase<EventDTO>, EventCreateUseCase>();
            services.AddScoped<IDeleteUseCase<EventDTO>, EventDeleteUseCase>();
            services.AddScoped<IUpdateUseCase<EventDTO>, EventUpdateUseCase>();
            services.AddScoped<IGetAllUseCase<EventDTO>, EventGetAllUseCase>();
            services.AddScoped<IGetPaginatedListUseCase<EventDTO>, EventGetPaginatedListUseCase>();
            services.AddScoped<IGetByIdUseCase<EventDTO>, EventGetByIdUseCase>();
            services.AddScoped<IGetEventByCategoryUseCase, EventGetByCategoryUseCase>();
            services.AddScoped<IGetEventByDateUseCase, EventGetByDateUseCase>();
            services.AddScoped<IGetEventByNameUseCase, EventGetByNameUseCase>();
            services.AddScoped<IGetEventByVenueUseCase, EventGetByVenueUseCase>();
        }

        private static void AddEventUserScopes(IServiceCollection services)
        {
            services.AddScoped<IUpdateUseCase<EventUserDTO>, EventUserUpdateUseCase>();
            services.AddScoped<IGetAllUseCase<EventUserDTO>, EventUserGetAllUseCase>();
            services.AddScoped<IGetPaginatedListUseCase<EventUserDTO>, EventUserGetPaginatedListUseCase>();
            services.AddScoped<IGetByIdUseCase<EventUserDTO>, EventUserGetByIdUseCase>();
            services.AddScoped<IGetUsersOfEventUseCase, EventUserGetUsersOfEventUseCase>();
            services.AddScoped<IRegisterUserInEventUseCase, EventUserRegisterUserInEventUseCase>();
            services.AddScoped<IUnregisterUserInEventUseCase, EventUserUnregisterUserInEventUseCase>();
        }

        private static void AddUserScopes(IServiceCollection services)
        {
            services.AddScoped<ICreateUseCase<UserDTO>, UserCreateUseCase>();
            services.AddScoped<IDeleteUseCase<UserDTO>, UserDeleteUseCase>();
            services.AddScoped<IUpdateUseCase<UserDTO>, UserUpdateUseCase>();
            services.AddScoped<IGetAllUseCase<UserDTO>, UserGetAllUseCase>();
            services.AddScoped<IGetPaginatedListUseCase<UserDTO>, UserGetPaginatedListUseCase>();
            services.AddScoped<IGetByIdUseCase<UserDTO>, UserGetByIdUseCase>();
        }
    }
}
