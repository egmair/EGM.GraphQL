using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using EGM.GQL.DataAccess.Primitives.Entities.Base;

namespace EGM.GQL.DataAccess.Primitives.Entities
{
    [Table("people")]
    public sealed class DbPerson : AuditableEntity
    {
        [Column("FIRST_NAME")]
        public string FirstName { get; set; }

        [Column("MIDDLE_NAME")]
        public string MiddleName { get; set; }

        [Column("LAST_NAME")]
        public string LastName { get; set; }

        [Column("E_MAIL_ADDRESS")]
        [EmailAddress]
        public string EmailAddress { get; set; }
        
        [Column("DATE_OF_BIRTH")]
        public DateTime DateOfBirth { get; set; }

        [ForeignKey(nameof(SexId))]
        public DbSex Sex { get; set; }
        
        [Column("SEX")]
        public Guid SexId { get; set; }
    }
}