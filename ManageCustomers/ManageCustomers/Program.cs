using ManageCustomers.Db;
using ManageCustomers.Domain;
using ManageCustomers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<CustomerDbContext>(opt => opt.UseInMemoryDatabase("InMem"));

builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


//Seeds some data when app starts
using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<CustomerDbContext>();
    DummyData.SeedData(context);
}

app.Run();
