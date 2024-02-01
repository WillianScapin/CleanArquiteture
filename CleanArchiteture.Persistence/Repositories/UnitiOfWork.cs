using CleanArchiteture.Domain.Interfaces;
using CleanArchiteture.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Persistence.Repositories
{
    public class UnitiOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitiOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task Commit(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
