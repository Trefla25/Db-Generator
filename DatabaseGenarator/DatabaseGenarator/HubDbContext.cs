using DatabaseGenarator.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DatabaseGenarator;
public class HubDbContext : DbContext
{
    public HubDbContext() : base()
    {

    }

    public HubDbContext(DbContextOptions<HubDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=C:\\Work\\Databases\\Device01.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Packet>()
            .HasIndex(p => p.DateCreated);
    }

    public DbSet<Packet> Packet { get; set; }
}