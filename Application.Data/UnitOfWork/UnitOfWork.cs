using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Data.DataContext;
using Application.Data.Repository;

namespace Application.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private IUserRepository _users;
        public IUserRepository Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRepository(_context);
                }
                return _users;
            }
        }

        private ITaskRepository _task;
        public ITaskRepository Tasks
        {
            get
            {
                if (_task == null)
                {
                    _task = new TaskRepository(_context);
                }
                return _task;
            }
        }

        private IValueRepository _value;
        public IValueRepository Values
        {
            get
            {
                if (_value == null)
                {
                    _value = new ValueRepository(_context);
                }
                return _value;
            }
        }

        public int Save()
        {
            int result = 0;

            try
            {
                result = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return result;
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            int result = 0;

            try
            {
                result = await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
