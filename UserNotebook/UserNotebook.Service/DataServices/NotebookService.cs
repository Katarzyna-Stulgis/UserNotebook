using ClosedXML.Excel;
using UserNotebook.Domain.Interfaces;
using UserNotebook.Domain.Models.Entities;
using UserNotebook.Domain.Models.Enums;

namespace UserNotebook.Service.DataServices
{
    public class NotebookService : INotebookService
    {
        private readonly INotebookRepository _repository;

        public NotebookService(INotebookRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> AddUserAsync(User user)
        {
            return await _repository.AddUserAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _repository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _repository.GetUserByIdAsync(id);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _repository.UpdateUserAsync(user);
        }

        public async Task<XLWorkbook> GenerateWorkbook()
        {
            var users = await GetAllUsersAsync();
            var numberOfUser = users.Count();

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Raport");

            worksheet.Cell(1, 1).Value = "Imię";
            worksheet.Cell(1, 2).Value = "Nazwisko";
            worksheet.Cell(1, 3).Value = "Data Urodzenia";
            worksheet.Cell(1, 4).Value = "Płeć";
            worksheet.Cell(1, 5).Value = "Tytuł";
            worksheet.Cell(1, 6).Value = "Wiek";

            for (int i = 0; i < numberOfUser; i++)
            {
                var user = users.ElementAt(i);
                var row = i + 2;

                worksheet.Cell(row, 1).Value = user.FirstName;
                worksheet.Cell(row, 2).Value = user.LastName;
                worksheet.Cell(row, 3).Value = user.BirthDate.ToShortDateString();
                worksheet.Cell(row, 4).Value = user.Gender.GetDescription();
                worksheet.Cell(row, 5).Value = Enum.GetName(typeof(Gender), user.Gender) == Gender.female.ToString() ? "Pani" : "Pan";

                var age = DateTime.Now.Year - user.BirthDate.Year;
                if (DateTime.Now < user.BirthDate.AddYears(age))
                {
                    age--;
                }

                worksheet.Cell(row, 6).Value = age;
            }

            return workbook;
        }
    }
}
