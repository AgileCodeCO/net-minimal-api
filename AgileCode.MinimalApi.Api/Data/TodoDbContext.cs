using AgileCode.MinimalApi.Api.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AgileCode.MinimalApi.Api.Data;

public class TodoDbContext : DbContext
{
    public DbSet<TodoItem> Todos => Set<TodoItem>();

    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }
}