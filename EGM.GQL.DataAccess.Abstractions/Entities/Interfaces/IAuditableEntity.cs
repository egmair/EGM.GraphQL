using System;

namespace EGM.GQL.DataAccess.Abstractions.Entities.Interfaces
{
    public interface IAuditableEntity : IEntityBase
    {
        /// <summary>
        /// The date and time the entity was created.
        /// </summary>
        DateTime? Created { get; set; }
        
        /// <summary>
        /// Who created the record (if applicable).
        /// </summary>
        string CreatedBy { get; set; }
        
        /// <summary>
        /// The date and time the entity was modified (if applicable).
        /// </summary>
        DateTime? Updated { get; set; }
        
        /// <summary>
        /// Who modified the record (if applicable). 
        /// </summary>
        string UpdatedBy { get; set; }
    }
}