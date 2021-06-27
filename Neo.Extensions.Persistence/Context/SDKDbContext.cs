using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Neo.Extensions.Persistence.ModelConfiguration;

namespace Neo.Extensions.Persistence.Context
{
    public class SDKDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public SDKDbContext(DbContextOptions<SDKDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            // Inject all map classes of EF
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LogEmailModelConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
