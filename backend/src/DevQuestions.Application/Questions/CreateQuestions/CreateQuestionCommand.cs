using DevQuestions.Application.Questions.Abstractions;
using DevQuestions.Contracts.Questions;

namespace DevQuestions.Application.Questions.CreateQuestions;

public record CreateQuestionCommand(CreateQuestionDto QuestionDto) : ICommand;