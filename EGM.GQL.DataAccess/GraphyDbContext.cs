using EGM.GQL.DataAccess.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace EGM.GQL.DataAccess
{
    public sealed class GraphyDbContext : DbContext
    {
        public GraphyDbContext()
        {
            
        }
        public GraphyDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<DbPerson>()
                .HasOne(person => person.Sex);
            modelBuilder.Entity<DbSex>();
            
        }
    }
}