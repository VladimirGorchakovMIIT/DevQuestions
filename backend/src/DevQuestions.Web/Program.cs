using DevQuestions.Web;
using DevQuestions.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgrammeDependencies();

var app = builder.Build();
app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "DevQuestions"));
}

app.MapControllers();
app.Run();