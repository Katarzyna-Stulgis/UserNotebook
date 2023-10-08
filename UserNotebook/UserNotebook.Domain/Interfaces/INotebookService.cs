using ClosedXML.Excel;
using UserNotebook.Domain.Models.Entities;

namespace UserNotebook.Domain.Interfaces
{
    public interface INotebookService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<XLWorkbook> GenerateWorkbook();
    }
}
