using AgileCode.MinimalApi.Api.Data;
using AgileCode.MinimalApi.Api.Data.Model;
using AgileCode.MinimalApi.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITodoService, TodoService>();

builder.Services.AddDbContext<TodoDbContext>(options =>
{
    var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    options.UseSqlite($"Data Source={Path.Join(path, "dbMinimalApi.db")}");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<TodoDbContext>();
db?.Database.EnsureCreated();

// APIs goes here
app.MapGet("/todo/{id}", async (int id, ITodoService todoService) => {
    var item = await todoService.GetItemAsync(id);
    if (item is null)
        return Results.NotFound();

    return Results.Ok(item);
});

app.MapPost("/todo", async (TodoItem item, ITodoService todoService) =>
{
    if (item is null)
        return Results.BadRequest("Body is null");

    if (string.IsNullOrWhiteSpace(item.Title))
        return Results.BadRequest("Title is null");

    var result = await todoService.AddItemAsync(item.Title);
    return Results.Created($"/todo/{result.Id}", result);
});

app.MapGet("/todo", async (ITodoService todoService) => {
    return Results.Ok(await todoService.GetItemsAsync());
});

app.MapPut("/todo/{id}", async (int id, TodoItem item, ITodoService todoService) => {
    var existingItem = await todoService.GetItemAsync(id);

    if (existingItem is null)
        return Results.NotFound();

    if (string.IsNullOrWhiteSpace(item.Title))
        return Results.BadRequest("Title is null");

    return Results.Ok(todoService.UpdateItemAsync(id, item.Title!));
});

app.MapPut("/todo/{id}/done", async (int id, ITodoService todoService) => {
    var existingItem = await todoService.GetItemAsync(id);

    if (existingItem is null)
        return Results.NotFound();

    return Results.Ok(todoService.MarkAsDoneAsync(id));
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

public partial class Program { }