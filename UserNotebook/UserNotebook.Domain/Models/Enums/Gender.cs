using System.ComponentModel;

namespace UserNotebook.Domain.Models.Enums
{
    public enum Gender
    {
        [Description("Kobieta")]
        female = 1,
        [Description("Mężczyzna")]
        male = 2
    }
}
