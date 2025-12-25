using DevQuestions.Contracts.Questions;
using DevQuestions.Contracts.Questions.Dtos;
using FluentValidation;

namespace DevQuestions.Application.Questions.CreateQuestions;

public class CreateQuestionValidator : AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Заголовок не валидный").MaximumLength(500).WithMessage("Заголовок слишком длинным.");
        RuleFor(x => x.Description).NotEmpty().MaximumLength(5000).WithMessage("Текст не валидный.");
        RuleFor(x => x.UserId).NotEmpty();
    }
}