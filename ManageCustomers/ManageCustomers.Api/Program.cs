using ManageCustomers.Domain.Models;
using ManageCustomers.Domain;
using Microsoft.EntityFrameworkCore;
using ManageCustomers.Db;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CustomerDbContext>(opt => opt.UseInMemoryDatabase("InMem"));

builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();

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

//Seeds some data when app starts
using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<CustomerDbContext>();
    DummyData.SeedData(context);
}

app.Run();
