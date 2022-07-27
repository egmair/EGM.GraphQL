using System.ComponentModel.DataAnnotations;

namespace EGM.GQL.DataAccess.Primitives.Enums
{
    public enum AuditActions : byte
    {
        [Display(Name = "CREATE")]
        Create = 1,
        
        [Display(Name = "UPDATE")]
        Update = 2,
        
        [Display(Name = "DELETE")]
        Delete = 3
    }
}