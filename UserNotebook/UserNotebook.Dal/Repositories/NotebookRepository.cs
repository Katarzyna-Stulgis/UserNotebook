using Microsoft.EntityFrameworkCore;
using UserNotebook.Dal.Context;
using UserNotebook.Domain.Interfaces;
using UserNotebook.Domain.Models.Entities;

namespace UserNotebook.Dal.Repositories
{
    public class NotebookRepository : INotebookRepository
    {
        private readonly UserNotebookDbContext _dbContext;

        public NotebookRepository(UserNotebookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _dbContext.Set<User>().Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Set<User>().ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var user = await _dbContext.Set<User>()
                 .Where(c => c.Id == id)
                 .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}
