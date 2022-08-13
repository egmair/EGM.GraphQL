using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EGM.GQL.DataAccess.Abstractions.Entities.Interfaces;

namespace EGM.GQL.DataAccess.Abstractions.Entities.Base
{
    public class EntityBase : IEntityBase
    {
        [Key]
        [Column("ID")]
        [Required]
        public Guid Id { get; set; }
    }
}