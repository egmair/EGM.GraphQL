using System;
using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.DataAccess.Abstractions.Entities;

namespace EGM.GQL.DataAccess.Abstractions.Repositories
{
    public interface IRepository<in TEntity> where TEntity : IEntityBase
    {
        /// <summary>
        /// Inserts a new record into the database.
        /// </summary>
        /// <param name="entity">The entity to be inserted.</param>
        /// <param name="createdBy">Who created the entity.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the work being carried out.</returns>
        Task InsertAsync(TEntity entity, string createdBy = "", CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing record in the database.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        /// <param name="modifiedBy">Who modified the entity.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the work being carried out.</returns>
        void Update(TEntity entity, string modifiedBy = "");
        
        /// <summary>
        /// Deletes a record from the database.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the work being carried out.</returns>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes a record from the database.
        /// </summary>
        /// <param name="entityId">The Id of the entity to be deleted.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the work being carried out.</returns>
        Task DeleteAsync(Guid entityId, CancellationToken cancellationToken = default);
    }
}