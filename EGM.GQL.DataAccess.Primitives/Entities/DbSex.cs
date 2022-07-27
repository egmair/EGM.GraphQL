using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EGM.GQL.DataAccess.Primitives.Entities.Base;

namespace EGM.GQL.DataAccess.Primitives.Entities
{
    [Table("sex")]
    public sealed class DbSex : EntityBase
    {
        [Column("DESCRIPTION")]
        [StringLength(10)]
        public string Description { get; set; }

        [Column("SHORT_DESCRIPTION")]
        [StringLength(2)]
        public string ShortDescription { get; set; }
    }
}