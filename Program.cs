using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
  options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddValidation();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/get-todo-list", async context => await new GetToDoListHandler().Handle(context));

app.Run();

[JsonSerializable(typeof(GetToDoListRequestModel))]
[JsonSerializable(typeof(GetToDoListResponseModel))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
