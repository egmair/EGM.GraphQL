using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.Abstractions.Services;
using EGM.GQL.Primitives.Models;
using HotChocolate;

namespace EGM.GQL.Queries.Queries
{
    public sealed class PersonQuery
    {
        #nullable enable
        
        public async Task<Person?> GetPersonByIdAsync(Guid id, [Service] IPersonService personService,
            CancellationToken cancellationToken = default)
        {
            var result = await personService.GetPersonByIdAsync(id, cancellationToken);
            return result.Match(person => person, exception => throw exception);
        }

        public async Task<IEnumerable<Person>> GetAllAsync([Service] IPersonService personService,
            CancellationToken cancellationToken = default)
        {
            var result = await personService.GetAllPeopleAsync(cancellationToken: cancellationToken);
            return result.Match(people => people, exception => throw exception);
        }

        #nullable disable
    }
}