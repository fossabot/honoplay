using Honoplay.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

#nullable enable
namespace Honoplay.Application.Tests
{
    public abstract class TestBase
    {

        protected HonoplayDbContext GetDbContext()
        {
            var builder = new DbContextOptionsBuilder<HonoplayDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new HonoplayDbContext(builder.Options);

            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}
