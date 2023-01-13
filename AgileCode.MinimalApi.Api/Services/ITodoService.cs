using System;
using AgileCode.MinimalApi.Api.Data.Model;

namespace AgileCode.MinimalApi.Api.Services
{
	public interface ITodoService
	{
        Task<TodoItem> AddItemAsync(string title);
        Task<TodoItem?> GetItemAsync(int id);
        Task<List<TodoItem>> GetItemsAsync();
        Task<TodoItem?> MarkAsDoneAsync(int id);
        Task<TodoItem?> UpdateItemAsync(int id, string title);
    }
}

