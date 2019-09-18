using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Data.DataContext;
using Application.Model;

namespace Application.Data.Repository
{
    public class ValueRepository: Repository<Value>, IValueRepository
    {
        public ValueRepository(ApplicationDbContext context):base(context)
        {

        }

        public ApplicationDbContext TaskManagementDbContext
        {
            get { return _context as ApplicationDbContext; }
        }

        public async Task<bool> IsDuplicateAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _context.Values.AnyAsync(u => u.Name == name);
            return result;
        }
    }
}
