using System;

namespace EGM.GQL.Primitives.Models.Base
{
    public class AuditableRecord : BaseRecord
    {
        public DateTime? Created { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime? Updated { get; set; }
        
        public string UpdatedBy { get; set; }
    }
}