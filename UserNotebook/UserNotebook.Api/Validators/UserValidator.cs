using FluentValidation;
using UserNotebook.Domain.Models.Entities;

namespace UserNotebook.Api.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .Must(BeOnlyLetters);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(150)
                .Must(BeOnlyLetters);

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .LessThan(DateTime.Now);

            RuleFor(x => x.PhoneNumber)
                .Must(BeNullOrHave9Digits);

            RuleFor(x => x.Gender)
                .NotEmpty();

            RuleFor(x => x.Position)
                .MaximumLength(500);
        }

        private bool BeOnlyLetters(string value)
        {
            return !string.IsNullOrEmpty(value) && value.All(char.IsLetter);
        }

        private bool BeNullOrHave9Digits(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            return System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d{9}$");
        }
    }
}
