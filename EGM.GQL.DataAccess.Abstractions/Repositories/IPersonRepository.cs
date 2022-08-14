using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.DataAccess.Abstractions.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace EGM.GQL.DataAccess.Abstractions.Repositories
{
    public interface IPersonRepository : IRepository<DbPerson>
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
        Task<IQueryable<DbPerson>> GetByEmailAsync(string emailAddress,
            Func<IQueryable<DbPerson>, IOrderedQueryable<DbPerson>> orderBy = null,
            Func<IQueryable<DbPerson>, IIncludableQueryable<DbPerson, object>> include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default);
    }
}