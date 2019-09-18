using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Data.DataContext;
using Application.Model;

namespace Application.Data.Repository
{
    public class TaskRepository : Repository<UserTask>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) : base(context)
        {

        }

        public ApplicationDbContext TaskManagementDbContext
        {
            get { return _context as ApplicationDbContext; }
        }

        public async Task<IEnumerable<UserTask>> GetAllTaskWithUser()
        {            
            var tasks = await _context.Tasks
                           .Include(u => u.User)                           
                           .ToListAsync();

            return tasks;
        }

        public async Task<UserTask> GetTaskWithUser(int id)
        {
            var task = await _context.Tasks
                           .Include(u => u.User)
                           .FirstOrDefaultAsync();

            return task;
        }

        public async Task<IEnumerable<UserTask>> GetAllTaskWithUser(int id) {
            var tasks = await _context.Tasks
                           .Include(u => u.User)
                           .Where(u => u.UserId == id)
                          .ToListAsync();

            return tasks;
        }

        public async Task<List<UserTask>> GetAllTaskByUserId(int id)
        {
            var tasks = from user in _context.Users
                        join task in _context.Tasks
                        on user.Id equals task.UserId
                        where user.Id == id select new UserTask{
                            Id = task.Id,
                            Name = task.Name,
                            Description = task.Description,
                            StartDate = task.StartDate,
                            EndDate = task.EndDate,
                            UserId = task.UserId,
                            UserName = user.FirstName + " " + user.LastName
                        };            

            return await tasks.ToListAsync();
        }

        public async Task<bool> IsExistTaskName(string name)
        {
            if (await _context.Tasks.AsNoTracking().AnyAsync(t => t.Name == name))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsExistTaskName(string oldName, string name)
        {
            bool status = false;

            var tasks = await _context.Tasks.AsNoTracking().Where(x => x.Name != oldName).ToListAsync();
            if (tasks != null && tasks.Count() > 0)
            {
                var task = await _context.Tasks.AsNoTracking().Where(x => x.Name == name).FirstOrDefaultAsync();
                if (task != null)
                {
                    status = true;
                }
            }

            return status;
        }        
    }
}
