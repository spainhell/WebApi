using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApi.DataLayerEF.Database;

namespace WebApi.UnitTests
{
    public static class DbContextMocker
    {
        public static DataContext GetDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new DataContext(options);

            // Add entities into memory
            dbContext.Seed();

            return dbContext;
        }
    }
}
