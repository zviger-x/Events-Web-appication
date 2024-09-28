namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    internal interface IRegisterUserInEventUseCase
    {   
        /// <summary>
        /// Registers a user in an event
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="eventId">Event id</param>
        /// <param name="registrationDate">Date of registration</param>
        /// <returns></returns>
        Task RegisterUserInEvent(int userId, int eventId, DateTime registrationDate);
    }
}
