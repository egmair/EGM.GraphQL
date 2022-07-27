using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.DataAccess.Abstractions.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace EGM.GQL.DataAccess.Abstractions.Repositories
{
    public interface IPersonRepository<TEntity> : IRepository<TEntity> where TEntity : IEntityBase
    {
        /// <summary>
        /// Finds records based on a supplied email address.
        /// </summary>
        /// <param name="emailAddress">The email address to look for.</param>
        /// <param name="orderBy">An optional function to order records.</param>
        /// <param name="include">An optional function to include additional data.</param>
        /// <param name="disableTracking">Whether tracking should be disabled or not.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>An <see cref="IList{T}"/> of records.</returns>
        Task<IList<TEntity>> GetByEmailAsync(string emailAddress,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default);
    }
}