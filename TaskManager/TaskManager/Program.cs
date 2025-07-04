using FluentValidation.AspNetCore;
using TaskListManager.Application.Services;
using TaskManager.Infrastructure;
using YourNamespace.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient<ITaskRepository, TaskRepository>(client =>
{
    client.BaseAddress = new Uri("https://68655ded5b5d8d033980e6ad.mockapi.io/api/v1/tasks");
});


builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<TaskItemValidator>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

