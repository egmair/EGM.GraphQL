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
                .UseProjection()
                .Type<PersonType>();

            descriptor
                .Field(p => p.GetAllAsync(default!, default!))
                .UseProjection()
                .Type<ListType<PersonType>>();
        }
    }
}