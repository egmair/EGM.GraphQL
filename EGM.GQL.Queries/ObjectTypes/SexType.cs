using EGM.GQL.DataAccess.Abstractions.Entities;
using EGM.GQL.Primitives.Models;
using HotChocolate.Types;

namespace EGM.GQL.Queries.ObjectTypes
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class SexType : ObjectType<DbSex>
    {
        protected override void Configure(IObjectTypeDescriptor<DbSex> descriptor)
        {
            descriptor
                .Field(s => s.Description)
                .Type<StringType>();

            descriptor
                .Field(s => s.ShortDescription)
                .Type<StringType>();

            descriptor
                .Field(s => s.Id)
                .Ignore();
        }
    }
}