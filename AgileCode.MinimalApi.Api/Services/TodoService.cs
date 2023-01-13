using System;
using AgileCode.MinimalApi.Api.Data;
using AgileCode.MinimalApi.Api.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AgileCode.MinimalApi.Api.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _dbContext;

        public TodoService(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TodoItem> AddItemAsync(string title)
        {
            var item = new TodoItem
            {
                Title = title,
                CreatedTime = DateTime.Now
            };

            await _dbContext.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<TodoItem?> GetItemAsync(int id)
        {
            return await _dbContext.Todos.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TodoItem>> GetItemsAsync()
        {
            return await _dbContext.Todos.ToListAsync();
        }

        public async Task<TodoItem?> MarkAsDoneAsync(int id)
        {
            var item = await _dbContext.Todos.FirstOrDefaultAsync(t => t.Id == id);
            if (item is null)
                return null;

            item.Completed = true;
            item.UpdatedTime = DateTime.Now;
            item.CompletedTime = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<TodoItem?> UpdateItemAsync(int id, string title)
        {
            var item = await _dbContext.Todos.FirstOrDefaultAsync(t => t.Id == id);
            if (item is null)
                return null;

            item.Title = title;
            item.UpdatedTime = DateTime.Now;            

            await _dbContext.SaveChangesAsync();

            return item;
        }
    }
}

