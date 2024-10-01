using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;
using EventsManagement.DataObjects.Utilities.Interfaces;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    internal interface IGetPaginatedListUseCase<T>
        where T : IEntityDTO
    {
        /// <summary>
        /// Returns a paginated list of elements
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        Task<IPaginatedList<T>> GetPaginatedListAsync(int pageIndex, int pageSize);
    }
}
