using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.DataAccess.Abstractions.Entities.Base;
using EGM.GQL.DataAccess.Abstractions.Entities.Interfaces;
using EGM.GQL.DataAccess.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace EGM.GQL.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly GraphyDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        
        public Repository(GraphyDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }
        
        public async Task InsertAsync(TEntity entity, string createdBy = "",
            CancellationToken cancellationToken = default)
        {
            if (typeof(IAuditableEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ((IAuditableEntity)entity).Created = DateTime.UtcNow;
                ((IAuditableEntity)entity).CreatedBy = createdBy;
            }

            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public void Update(TEntity entity, string updatedBy = "")
        {
            if (typeof(IAuditableEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ((IAuditableEntity)entity).Updated = DateTime.UtcNow;
                ((IAuditableEntity)entity).UpdatedBy = updatedBy;
            }
            
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity) => _dbSet.Remove(entity);

        public async Task DeleteAsync(Guid entityId, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync(entityId, cancellationToken);

            if (entity is null)
            {
                throw new InvalidOperationException($"Could not find entity with Id: {entityId}");
            }

            _dbSet.Remove(entity);
        }

        public TEntity Find(params object[] keyValues) => _dbSet.Find(keyValues);

        public async Task<TEntity> FindAsync(CancellationToken cancellationToken = default, params object[] keyValues)
            => await _dbSet.FindAsync(keyValues, cancellationToken);

        public Task<IQueryable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default)
            => Task.FromResult(BuildQuery(null, orderBy, include, disableTracking));

        public Task<IQueryable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default)
            => Task.FromResult(BuildQuery(where, orderBy, include, disableTracking));

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> selector,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default)
            => await BuildQuery(where, orderBy, include, disableTracking).SingleOrDefaultAsync(cancellationToken);

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> selector,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default)
            => await BuildQuery(where, orderBy, include, disableTracking).FirstOrDefaultAsync(cancellationToken);

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where = null,
            CancellationToken cancellationToken = default)
            => await BuildQuery(where).CountAsync(cancellationToken);

        public async Task<bool> DoesExistAsync(Expression<Func<TEntity, bool>> where = null,
            CancellationToken cancellationToken = default)
            => await BuildQuery(where).AnyAsync(cancellationToken);

        protected virtual IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (!(include is null))
            {
                query = include(query);
            }

            if (!(where is null))
            {
                query = query.Where(where);
            }

            if (!(orderBy is null))
            {
                query = orderBy(query);
            }

            return query;
        }
    }
}