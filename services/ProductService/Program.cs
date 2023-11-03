using BTH.H23.PA2577.Assignment1.ProductService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbConnString = Environment.GetEnvironmentVariable("DB_ConnectionString") ?? builder.Configuration.GetConnectionString("DefaultConnection");

// Inside ConfigureServices method
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(dbConnString));

var app = builder.Build();

var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
var ctx = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

await ctx.Database.MigrateAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
