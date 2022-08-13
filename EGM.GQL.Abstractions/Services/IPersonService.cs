using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.DataAccess.Abstractions.Entities;
using EGM.GQL.Primitives.Models;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore.Query;

namespace EGM.GQL.Abstractions.Services
{
    public interface IPersonService
    {
        Task<Result<IList<Person>>> GetAllPeopleAsync(
            Func<IQueryable<DbPerson>, IOrderedQueryable<DbPerson>> orderBy = null,
            Func<IQueryable<DbPerson>, IIncludableQueryable<DbPerson, object>> include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default);

        Task<Result<Person>> GetPersonByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}