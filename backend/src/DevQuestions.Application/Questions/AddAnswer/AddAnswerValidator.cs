using DevQuestions.Contracts.Questions;
using DevQuestions.Contracts.Questions.Dtos;
using FluentValidation;

namespace DevQuestions.Application.Questions.AddAnswer;

public class AddAnswerValidator : AbstractValidator<AddAnswerDto>
{
    public AddAnswerValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Text).NotEmpty().WithMessage("Текст не должен быть пустым").MaximumLength(5000).WithMessage("Размер текста не должен превышать 5000 символов");
    }
}