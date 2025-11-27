using DevQuestions.Contracts.Questions;
using FluentValidation;

namespace DevQuestions.Application.Questions;

public class CreateQuestionValidator : AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Заголовок не валидный").MaximumLength(500).WithMessage("Заголовок слишком длинным.");
        RuleFor(x => x.Description).NotEmpty().MaximumLength(5000).WithMessage("Текст не валидный.");
        RuleFor(x => x.UserId).NotEmpty();
    }
}