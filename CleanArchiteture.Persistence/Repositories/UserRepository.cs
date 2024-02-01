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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User> GetByEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(w => w.Email == email, cancellationToken);
        }
    }
}
