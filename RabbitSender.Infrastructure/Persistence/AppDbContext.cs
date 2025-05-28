using Microsoft.EntityFrameworkCore;
using RabbitSender.Domain.Entities;

namespace RabbitSender.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<EmailMessage> EmailMessages => Set<EmailMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailMessage>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.To).IsRequired();
            entity.Property(e => e.Subject).IsRequired();
            entity.Property(e => e.Body).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
