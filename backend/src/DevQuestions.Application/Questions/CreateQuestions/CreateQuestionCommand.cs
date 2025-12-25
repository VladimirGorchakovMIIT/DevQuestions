using DevQuestions.Application.Questions.Abstractions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Contracts.Questions.Dtos;

namespace DevQuestions.Application.Questions.CreateQuestions;

public record CreateQuestionCommand(CreateQuestionDto QuestionDto) : ICommand;