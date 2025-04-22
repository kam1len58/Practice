using Microsoft.Extensions.Options;
using taak7.StringProcessingService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Task8>(builder.Configuration.GetSection("Task8"));

builder.Services.AddScoped<StringProcessingService>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseRouting();
app.UseAuthorization();

app.MapControllers();


app.Run();
public class AppSettings
{
    public string AllowedHosts { get; set; }
    public Task8 Task8 { get; set; }
}

public class Task8
{
    public string RandomApi { get; set; }
    public List<string> BlackList { get; set; }
}

public class MyService
{
    private readonly Task8 _myOptions;

    public MyService(IOptions<Task8> myOptionsAccessor)
    {
        _myOptions = myOptionsAccessor.Value;
    }
}












