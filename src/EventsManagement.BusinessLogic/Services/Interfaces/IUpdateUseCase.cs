﻿using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IUpdateUseCase<T>
        where T : IEntityDTO
    {
        /// <summary>
        /// Updates an entity in the repository.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        Task UpdateAsync(T entity);
    }
}
