using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.EventService;
using EventsManagement.BusinessLogic.Services.EventUserService;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Services.UserService;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.Repositories;
using EventsManagement.DataAccess.Repositories.Interfaces;

namespace EventsManagement.WebAPI.Server
{
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
            services.AddScoped<IUserRepository, UserRepository>();
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
            services.AddScoped<IGetEventsByCategoryUseCase, EventGetByCategoryUseCase>();
            services.AddScoped<IGetEventsByDateUseCase, EventGetByDateUseCase>();
            services.AddScoped<IGetEventByNameUseCase, EventGetByNameUseCase>();
            services.AddScoped<IGetEventsByVenueUseCase, EventGetByVenueUseCase>();
        }

        private static void AddEventUserScopes(IServiceCollection services)
        {
            services.AddScoped<IUpdateUseCase<EventUserDTO>, EventUserUpdateUseCase>();
            services.AddScoped<IGetAllUseCase<EventUserDTO>, EventUserGetAllUseCase>();
            services.AddScoped<IGetPaginatedListUseCase<EventUserDTO>, EventUserGetPaginatedListUseCase>();
            services.AddScoped<IGetByIdUseCase<EventUserDTO>, EventUserGetByIdUseCase>();
            services.AddScoped<IGetUsersOfEventUseCase, EventUserGetUsersOfEventUseCase>();
            services.AddScoped<IGetEventsOfUserUseCase, EventUserGetEventsOfUserUseCase>();
            services.AddScoped<IRegisterUserInEventUseCase, EventUserRegisterUserInEventUseCase>();
            services.AddScoped<IUnregisterUserInEventUseCase, EventUserUnregisterUserInEventUseCase>();
            services.AddScoped<IEventUserCheckRegistrationUseCase, EventUserCheckRegistrationUseCase>();
            services.AddScoped<IEventUserGetByUserIdAndEventIdUseCase, EventUserGetByUserIdAndEventIdUseCase>();
        }

        private static void AddUserScopes(IServiceCollection services)
        {
            services.AddScoped<ICreateUseCase<UserDTO>, UserCreateUseCase>();
            services.AddScoped<IDeleteUseCase<UserDTO>, UserDeleteUseCase>();
            services.AddScoped<IUpdateUseCase<UserDTO>, UserUpdateUseCase>();
            services.AddScoped<IGetAllUseCase<UserDTO>, UserGetAllUseCase>();
            services.AddScoped<IGetPaginatedListUseCase<UserDTO>, UserGetPaginatedListUseCase>();
            services.AddScoped<IGetByIdUseCase<UserDTO>, UserGetByIdUseCase>();
            services.AddScoped<IGetUserByEmailUseCase, UserGetByEmailUseCase>();
            services.AddScoped<IVerifyUserPasswordUseCase, UserVerifyPasswordUseCase>();
        }
    }
}
