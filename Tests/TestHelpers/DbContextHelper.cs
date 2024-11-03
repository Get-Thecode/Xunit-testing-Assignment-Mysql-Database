using ClaimsManagementApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimsManagementApi.Tests.TestHelpers
{
    public class DbContextHelper
    {
        public static AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
