using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.DataAccess.Abstractions;
using EGM.GQL.DataAccess.Abstractions.Entities;
using HotChocolate;

namespace EGM.GQL.Queries.Queries
{
    public sealed class PersonQuery
    {
        #nullable enable
        
        // public async Task<Person?> GetPersonByIdAsync(Guid id, [Service] IPersonService personService,
        //     CancellationToken cancellationToken = default)
        // {
        //     person
        //     var result = await personService.GetPersonByIdAsync(id, cancellationToken);
        //     return result.Match(person => person, exception => throw exception);
        // }

        public async Task<IQueryable<DbPerson>> GetAllAsync([Service] IUnitOfWork unitOfWork,
            CancellationToken cancellationToken = default)
        {
            return await unitOfWork.People.GetAllAsync(cancellationToken: cancellationToken);
        }

        #nullable disable
    }
}