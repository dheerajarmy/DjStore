using DjStore.Data;
using DjStore.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DjStoreContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnect"));
});
builder.Services.AddCors();
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
});
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();



var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<DjStoreContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    //context.Database.Migrate();
    DBInitializer.Initialize(context);
}
catch (Exception e)
{
    logger.LogError(e, "Problem Occured ");
}
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
