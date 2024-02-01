using CleanArchiteture.Domain.Entities;
using CleanArchiteture.Domain.Interfaces;
using CleanArchiteture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            entity.DateCreated = DateTimeOffset.UtcNow;
            _context.Add(entity);
        }

        public void Update(T entity)
        {
            entity.DateUpdated = DateTimeOffset.UtcNow;
            _context.Update(entity);
        }

        public void Delete(T entity)
        {
            entity.DateDeleted = DateTimeOffset.UtcNow;
            _context.Remove(entity);
        }

        public async Task<T> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
        }

        public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

    }
}
