namespace AgileCode.MinimalApi.Api.Data.Model;

public class TodoItem
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool Completed { get; set; }
    public DateTime? CompletedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public DateTime CreatedTime { get; set; }
}