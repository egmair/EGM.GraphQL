using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.DataAccess.Abstractions.Entities.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace EGM.GQL.DataAccess.Abstractions.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntityBase
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
        /// <returns>A <see cref="Task"/> representing the work being carried out.</returns>
        void Update(TEntity entity, string modifiedBy = "");
        
        /// <summary>
        /// Deletes a record from the database.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <returns>A <see cref="Task"/> representing the work being carried out.</returns>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes a record from the database.
        /// </summary>
        /// <param name="entityId">The Id of the entity to be deleted.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the work being carried out.</returns>
        Task DeleteAsync(Guid entityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds an entity by its key.
        /// </summary>
        /// <param name="keyValues">The desired key objects to use to find the record.</param>
        /// <returns>A <typeparamref name="TEntity"/> or <c>null</c>.</returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// Finds an entity by its key.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <param name="keyValues">The desired key objects to use to find the record.</param>
        /// <returns>A <typeparamref name="TEntity"/> or <c>null</c>.</returns>
        Task<TEntity> FindAsync(CancellationToken cancellationToken = default, params object[] keyValues);

        /// <summary>
        /// Gets all records from the database.
        /// </summary>
        /// <param name="orderBy">An optional function to order the records.</param>
        /// <param name="include">An optional function to include additional data.</param>
        /// <param name="disableTracking">Whether tracking should be disabled or not.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>An <see cref="IQueryable{T}"/> of records.</returns>
        Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a collection of records from the database.
        /// </summary>
        /// <param name="where">An optional expression to filter records by.</param>
        /// <param name="orderBy">An optional function to order records by.</param>
        /// <param name="include">An optional function to include additional data.</param>
        /// <param name="disableTracking">Whether tracking should be disabled or not.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>An <see cref="IQueryable{T}"/> of records.</returns>
        Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a single record from the database.
        /// </summary>
        /// <param name="selector">An expression to filter records by.</param>
        /// <param name="where">An optional expression to filter records by.</param>
        /// <param name="orderBy">An optional function to order records by.</param>
        /// <param name="include">An optional function to include additional data.</param>
        /// <param name="disableTracking">Whether tracking should be disabled or not.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <typeparamref name="TEntity"/> or <c>null</c>.</returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> selector,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the first record from the database.
        /// </summary>
        /// <param name="selector">An expression to filter records by.</param>
        /// <param name="where">An optional expression to filter records by.</param>
        /// <param name="orderBy">An optional function to order records by.</param>
        /// <param name="include">An optional function to include additional data.</param>
        /// <param name="disableTracking">Whether tracking should be disabled or not.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <typeparamref name="TEntity"/> or <c>null</c>.</returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> selector,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Counts the number of records.
        /// </summary>
        /// <param name="where">An optional expression to filter by.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>An <see cref="int"/>, describing the number of records found.</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> where = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks for existing records.
        /// </summary>
        /// <param name="where">An optional expression to filter by.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="bool"/> value, <c>true</c> if found.</returns>
        Task<bool> DoesExistAsync(Expression<Func<TEntity, bool>> where = null,
            CancellationToken cancellationToken = default);
    }
}