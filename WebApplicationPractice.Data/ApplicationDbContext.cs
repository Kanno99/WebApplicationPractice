using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;
using WebApplicationPractice.Data.Entities;

namespace WebApplicationPractice.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        static ApplicationDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
#pragma warning disable EF1001 // Internal EF Core API usage.
            _connectionString = options.GetExtension<NpgsqlOptionsExtension>().ConnectionString;
#pragma warning restore EF1001 // Internal EF Core API usage.
        }

        // Tables
        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Set schema name for tables
            builder.HasDefaultSchema("Data");

            // Configure tables
            builder.ApplyConfiguration(new Customer.Configuration());
        }
    }
}
