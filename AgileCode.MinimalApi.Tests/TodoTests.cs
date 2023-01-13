using System;
using System.Net;
using System.Text;
using System.Text.Json;
using AgileCode.MinimalApi.Api.Data.Model;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AgileCode.MinimalApi.Tests
{
	public class TodoTests : IClassFixture<WebApplicationFactory<Program>>
	{
		private readonly WebApplicationFactory<Program> _factory;
		private readonly HttpClient _httpClient;

        public TodoTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task AddTodoItem_ReturnsCreatedSuccess()
        {
            //Assert
            var todoItem = new TodoItem { Title = "Item from tests" };
            var content = new StringContent(JsonSerializer.Serialize(todoItem), Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync("/todo/", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<TodoItem>(responseContent, new JsonSerializerOptions(System.Text.Json.JsonSerializerDefaults.Web));

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(item);
            Assert.NotNull(response.Headers.Location);            
        }
    }
}

