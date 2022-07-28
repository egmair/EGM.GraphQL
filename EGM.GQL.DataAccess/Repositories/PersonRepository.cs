using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.DataAccess.Abstractions.Repositories;
using EGM.GQL.DataAccess.Primitives;
using EGM.GQL.DataAccess.Primitives.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace EGM.GQL.DataAccess.Repositories
{
    internal sealed class PersonRepository : Repository<DbPerson>, IPersonRepository<DbPerson>
    {
        public PersonRepository(GraphyDbContextFactory factory) : base(factory)
        {
        }

        public async Task<IList<DbPerson>> GetByEmailAsync(string emailAddress,
            Func<IQueryable<DbPerson>, IOrderedQueryable<DbPerson>> orderBy = null,
            Func<IQueryable<DbPerson>, IIncludableQueryable<DbPerson, object>> include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default)
            => await GetAsync(p => p.EmailAddress.Equals(emailAddress, StringComparison.InvariantCultureIgnoreCase),
                orderBy, include, disableTracking, cancellationToken);
    }
}