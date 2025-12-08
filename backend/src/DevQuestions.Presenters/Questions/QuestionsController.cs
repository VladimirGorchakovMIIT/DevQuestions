using DevQuestions.Application.Questions;
using DevQuestions.Application.Questions.Abstractions;
using DevQuestions.Application.Questions.AddAnswer;
using DevQuestions.Application.Questions.CreateQuestions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Presenters.ResponseExtensions;
using Microsoft.AspNetCore.Mvc;

namespace DevQuestions.Presenters.Questions;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] ICommandHandler<Guid, CreateQuestionCommand> handler,
        [FromBody] CreateQuestionDto request, 
        CancellationToken cancellationToken
        )
    {
        var command = new CreateQuestionCommand(request);
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok("Questions created!");
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetQuestionRequestDto question, CancellationToken cancellationToken)
    {
        return Ok("Questions!");
    }

    [HttpGet("{questionId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid questionId)
    {
        return Ok("Question get by id!");
    }

    [HttpPut("{questionId:guid}")]
    public async Task<IActionResult> Put(
        [FromRoute] Guid questionId,
        [FromBody] UpdateQuestionDto request,
        CancellationToken cancellationToken)
    {
        return Ok("Questions updated!");
    }

    [HttpDelete("{questionId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid questionId, CancellationToken cancellationToken)
    {
        return Ok("Question deleted!");
    }

    [HttpPut("{questionId:guid}/solution")]
    public async Task<IActionResult> SelectSolution(
        [FromRoute] Guid questionId,
        [FromQuery] Guid correctAnswerId,
        CancellationToken cancellationToken)
    {
        return Ok("Solution selected!");
    }

    [HttpPut("{questionId:guid}/answers")]
    public async Task<IActionResult> AddAnswer(
        [FromRoute] Guid questionId,
        [FromBody] AddAnswerDto request,
        [FromServices] ICommandHandler<Guid, AddAnswerCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new AddAnswerCommand(questionId, request);
        var result = await handler.Handle(command, cancellationToken);
        
        if(result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok("Solution selected!");
    }
}