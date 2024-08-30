using ManageCustomers.Domain;
using ManageCustomers.Domain.Models;
using Microsoft.CodeAnalysis;
using System;

namespace ManageCustomers.Db
{
    public static class DummyData
    {
        public static void SeedData(CustomerDbContext dbContext)
        {

            dbContext.Customers.AddRange(new Customer { FirstName = "Vijay",LastName = "Kumar", DOB = new DateTime(1995, 4, 29), Address = "B16 3NH", Gender = 'M', Mobile = "07398279272" },
                new Customer { FirstName = "John", LastName ="Lewis" , DOB = new DateTime(1995, 4, 28), Address = "B20 5HS", Gender = 'F', Mobile = string.Empty },
                new Customer { FirstName = "Sam", LastName ="david", DOB = new DateTime(1995, 4, 27), Address = "B23 3HQ", Gender = 'M', Mobile = "07398279763" });
            dbContext.SaveChanges();

        }
    }
}
