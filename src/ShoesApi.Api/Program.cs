using ShoesApi.Api.Configurators;

var builder = WebApplication.CreateBuilder(args);

// Конфигурация всех сервисов
builder.ConfigureServices();

var app = builder.Build();

// Конфигурация веб приложения
app.ConfigureWebApp();

app.Run();










/// <summary>
/// Для интеграционных тестов
/// </summary>
public partial class Program { }
