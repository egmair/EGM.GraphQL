using EGM.GQL.Queries.ObjectTypes;
using EGM.GQL.Queries.Queries;
using HotChocolate.Types;

namespace EGM.GQL.Queries.QueryTypes
{
    public sealed class PersonQueryType : ObjectType<PersonQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<PersonQuery> descriptor)
        {
            descriptor
                .Field(p => p.GetPersonByIdAsync(default!, default!, default!))
                .Type<PersonType>();

            descriptor
                .Field(p => p.GetAllAsync(default!, default!))
                .Type<PersonType>();
        }
    }
}