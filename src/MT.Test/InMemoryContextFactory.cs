using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MT.Data.Context;
using System;

namespace MT.Test
{
    public static class InMemoryContextFactory
    {
        public static BContext Create()
        {
            var options = new DbContextOptionsBuilder<BContext>()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            return new BContext(options);
        }
    }
}
