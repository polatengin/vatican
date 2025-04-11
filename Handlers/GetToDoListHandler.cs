using System.ComponentModel.DataAnnotations;

public record ToDoItem(string CategoryId, string Id, string Title, string Description, DateTime DueDate);

public record GetToDoListRequestModel([Required]string? CategoryId, int Page, DateTime? DueDate);

public record GetToDoListResponseModel(List<ToDoItem> Data, int TotalCount);

public class GetToDoListHandler
{
  public async Task Handle(HttpContext context)
  {
    var request = await context.Request.ReadFromJsonAsync<GetToDoListRequestModel>();

    var random = new Random();

    var items = new List<ToDoItem>();

    items.AddRange(Enumerable.Range(1, Random.Shared.Next(5, 50)).Select(i => new ToDoItem(
      CategoryId: Guid.NewGuid().ToString(),
      Id: Guid.NewGuid().ToString(),
      Title: $"Task {i}",
      Description: $"Description {i}",
      DueDate: DateTime.Now.AddDays(random.Next(-30, 30))
    )));

    var response = new GetToDoListResponseModel(items, items.Count);

    context.Response.ContentType = "application/json";
    context.Response.StatusCode = StatusCodes.Status200OK;
    await context.Response.WriteAsJsonAsync(response, AppJsonSerializerContext.Default.GetToDoListResponseModel);
  }
}
