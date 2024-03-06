using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestAPITest.Data;
using RestAPITest.Data.Models;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(op =>
op.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));
var app = builder.Build();
//builder.Services.AddIdentity<APPUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services
    .AddIdentityApiEndpoints<APPUser>()
    .AddEntityFrameworkStores<AppDbContext>();
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
