using Catalog.Infra.Queries.Entities;
using Microsoft.EntityFrameworkCore;

namespace BC.TempateFull.Infra.Query;

internal class QueryDbContext(DbContextOptions<QueryDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(BC.TempateFull.Infra.Constants.Schema);
        modelBuilder.Entity<Todo>().HasMany(r => r.Items).WithOne().HasForeignKey(r => r.TodoId);
        base.OnModelCreating(modelBuilder);
    }
}