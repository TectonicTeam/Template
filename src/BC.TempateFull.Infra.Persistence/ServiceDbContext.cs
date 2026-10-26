using Dekacore.Persistence.EntityFramework;
using Dekacore.Persistence.EntityFramework.Auditing;
using Dekacore.Persistence.EntityFramework.BackgroundTask;
using Dekacore.Persistence.EntityFramework.Concurrency;
using BC.TempateFull.Infra.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BC.TempateFull.Infra.Persistence;

public class ServiceDbContext(DbContextOptions<ServiceDbContext> options) : ApplicationDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(BC.TempateFull.Infra.Constants.Schema);
        modelBuilder.ApplyConfiguration(new TodoMapping());
        modelBuilder.AddAuditableProperties();
        modelBuilder.AddRowVersion();
        modelBuilder.AddConcurrencyToken();
        modelBuilder.SeedData();
        modelBuilder.AddBackgroundJobStore();
        base.OnModelCreating(modelBuilder);
    }
}