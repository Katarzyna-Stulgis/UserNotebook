using UserNotebook.Domain.Models.Entities;

namespace UserNotebook.Domain.Interfaces
{
    public interface INotebookRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);

    }
}
