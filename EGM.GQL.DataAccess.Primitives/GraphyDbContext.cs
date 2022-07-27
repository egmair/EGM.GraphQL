using EGM.GQL.DataAccess.Primitives.Entities;
using Microsoft.EntityFrameworkCore;

namespace EGM.GQL.DataAccess.Primitives
{
    public class GraphyDbContext : DbContext
    {
        public GraphyDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}