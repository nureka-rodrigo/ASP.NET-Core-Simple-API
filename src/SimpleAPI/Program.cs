using SimpleAPI.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddScoped<UsersService>();
    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app.MapControllers();
}

app.Run();
