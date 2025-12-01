using DomainModel;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Имя сотрудника обязательно")
                .Length(2, 100).WithMessage("Имя должно быть от 2 до 100 символов")
                .Matches("^[a-zA-Zа-яА-ЯёЁ\\s-]+$").WithMessage("Имя содержит недопустимые символы");

            RuleFor(e => e.WorkExp)
                .GreaterThanOrEqualTo(0).WithMessage("Опыт работы не может быть отрицательным")
                .LessThanOrEqualTo(50).WithMessage("Опыт работы не может превышать 50 лет");

            RuleFor(e => e.Vacancy)
                .IsInEnum().WithMessage("Указана недопустимая должность");
        }
    }

    public class EmployeeCreateDtoValidator : AbstractValidator<(string name, int workExp, VacancyType vacancy)>
    {
        public EmployeeCreateDtoValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Имя сотрудника обязательно")
                .Length(2, 100).WithMessage("Имя должно быть от 2 до 100 символов");

            RuleFor(x => x.workExp)
                .GreaterThanOrEqualTo(0).WithMessage("Опыт работы не может быть отрицательным")
                .LessThanOrEqualTo(50).WithMessage("Опыт работы не может превышать 50 лет");
        }
    }
}