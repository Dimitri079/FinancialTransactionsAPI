using FinancialTransactionsAPI.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the AppDbContext with SQL Server (use your connection string here)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var conectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(conectionstring);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();