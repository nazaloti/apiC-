using Microsoft.EntityFrameworkCore;
using WFConfin.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connecetionString = builder.Configuration.GetConnectionString("ConnetctionPostgres");
builder.Services.AddDbContext<WFConFinDbContext>(x => x.UseNpgsql(connecetionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
