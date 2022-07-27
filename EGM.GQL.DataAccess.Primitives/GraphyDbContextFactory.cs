using System;
using EGM.GQL.DataAccess.Primitives.Entities;
using Microsoft.EntityFrameworkCore;

namespace EGM.GQL.DataAccess.Primitives
{
    public sealed class GraphyDbContextFactory : IDisposable
    {
        private bool _disposed;   
        private readonly Func<DbContext> _instanceFunc;   
        private DbContext _dbContext;   
        public DbContext Context => _dbContext ??= _instanceFunc.Invoke();
        
        public GraphyDbContextFactory(Func<DbContext> dbContextFactory)   
        {   
            _instanceFunc = dbContextFactory;   
        }
        
        public void Dispose()
        {
            if (_disposed || _dbContext is null) return;
            
            _disposed = true;   
            _dbContext.Dispose();
        }
    }
}