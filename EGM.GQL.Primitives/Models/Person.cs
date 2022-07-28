using System;
using EGM.GQL.Primitives.Models.Base;

namespace EGM.GQL.Primitives.Models
{
    public class Person : AuditableRecord
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public Sex Sex { get; set; }
        
        public Guid SexId { get; set; }
    }
}