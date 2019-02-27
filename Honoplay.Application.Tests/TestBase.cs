using Honoplay.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

#nullable enable

namespace Honoplay.Application.Tests
{
    public abstract class TestBase
    {
        protected HonoplayDbContext GetDbContext()
        {
            var builder = new DbContextOptionsBuilder<HonoplayDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            var dbContext = new HonoplayDbContext(builder.Options);

            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}