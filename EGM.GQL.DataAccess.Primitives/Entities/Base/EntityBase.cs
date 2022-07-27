using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EGM.GQL.DataAccess.Abstractions.Entities;

namespace EGM.GQL.DataAccess.Primitives.Entities.Base
{
    public class EntityBase : IEntityBase
    {
        [Key]
        [Column("ID")]
        [Required]
        public Guid Id { get; set; }
    }
}