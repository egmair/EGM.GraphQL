using EGM.GQL.Primitives.Models.Base;

namespace EGM.GQL.Primitives.Models
{
    public class Sex : BaseRecord
    {
        public string Description { get; set; }
        
        public string ShortDescription { get; set; }
    }
}