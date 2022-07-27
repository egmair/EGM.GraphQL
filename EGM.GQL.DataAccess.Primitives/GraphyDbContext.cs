using EGM.GQL.DataAccess.Primitives.Entities;
using Microsoft.EntityFrameworkCore;

namespace EGM.GQL.DataAccess.Primitives
{
    public sealed class GraphyDbContext : DbContext
    {
        public GraphyDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}