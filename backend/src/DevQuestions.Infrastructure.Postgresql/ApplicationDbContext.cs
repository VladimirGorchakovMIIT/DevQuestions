using DevQuestions.Domain.Questions;
using Microsoft.EntityFrameworkCore;

namespace DevQuestions.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Question> Questions { get; set; }
}