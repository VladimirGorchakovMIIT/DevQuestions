using DevQuestions.Application.Questions.Abstractions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Contracts.Questions.Dtos;

namespace DevQuestions.Application.Questions.AddAnswer;

public record AddAnswerCommand(Guid QuestionId, AddAnswerDto AddAnswerDto) : ICommand;