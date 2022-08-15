using EGM.GQL.DataAccess.Abstractions.Entities;
using EGM.GQL.Primitives.Models;
using HotChocolate.Types;

namespace EGM.GQL.Queries.ObjectTypes
{
    internal sealed class PersonType : ObjectType<DbPerson>
    {
        protected override void Configure(IObjectTypeDescriptor<DbPerson> descriptor)
        {
            descriptor
                .Field(f => f.Id)
                .Type<IdType>();

            descriptor
                .Field(f => f.FirstName)
                .Type<StringType>();
            
            descriptor
                .Field(f => f.MiddleName)
                .Type<StringType>();
            
            descriptor
                .Field(f => f.LastName)
                .Type<StringType>();
            
            descriptor
                .Field(f => f.EmailAddress)
                .Type<StringType>();

            descriptor
                .Field(f => f.SexId)
                .Ignore();
            
            descriptor
                .Field(f => f.DateOfBirth)
                .Type<DateType>();
            
            descriptor
                .Field(f => f.Sex)
                .Type<SexType>();
        }
    }
}