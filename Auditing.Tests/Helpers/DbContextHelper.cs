using Auditing.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Auditing.Tests.Helpers;

public static class DbContextHelper
{
    public static AuditingDbContext Create(string dbName)
    {
        var options = new DbContextOptionsBuilder<AuditingDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new AuditingDbContext(options);
    }
}