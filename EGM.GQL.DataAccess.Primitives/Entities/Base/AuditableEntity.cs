using System;
using System.ComponentModel.DataAnnotations.Schema;
using EGM.GQL.DataAccess.Abstractions.Entities;

namespace EGM.GQL.DataAccess.Primitives.Entities.Base
{
    public class AuditableEntity : EntityBase, IAuditableEntity
    {
        [Column("CREATED")]
        public DateTime? Created { get; set; }
        
        [Column("CREATED_BY")]
        public string CreatedBy { get; set; }
        
        [Column("MODIFIED")]
        public DateTime? Updated { get; set; }
        
        [Column("MODIFIED_BY")]
        public string UpdatedBy { get; set; }
    }
}