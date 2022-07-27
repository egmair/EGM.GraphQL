using System;

namespace EGM.GQL.DataAccess.Abstractions.Entities
{
    public interface IEntityBase
    {
        /// <summary>
        /// The entities unique identifier.
        /// </summary>
        Guid Id { get; set; }
    }
}