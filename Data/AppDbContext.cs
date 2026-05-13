using Cwiczenie7.Models;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenie7.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<PC> PCs { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<PCComponent> PCComponents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer { Id = 1, Abbreviation = "INTEL", FullName = "Intel Corporation", FoundationDate = new DateTime(1968, 7, 18) },
            new ComponentManufacturer { Id = 2, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateTime(1969, 5, 1) },
            new ComponentManufacturer { Id = 3, Abbreviation = "NVIDIA", FullName = "NVIDIA Corporation", FoundationDate = new DateTime(1993, 4, 5) }
        );
        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Central Processing Unit" },
            new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Processing Unit" },
            new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Random Access Memory" }
        );
        modelBuilder.Entity<Component>().HasData(
            new Component { Code = "INT-I9-14X", Name = "Intel Core i9-14900K", Description = "High end gaming CPU", ComponentManufacturersId = 1, ComponentTypesId = 1 },
            new Component { Code = "AMD-R9-79X", Name = "AMD Ryzen 9 7950X3D", Description = "Top tier AMD CPU", ComponentManufacturersId = 2, ComponentTypesId = 1 },
            new Component { Code = "NVD-RTX40X", Name = "NVIDIA GeForce RTX 4090", Description = "Best consumer GPU", ComponentManufacturersId = 3, ComponentTypesId = 2 }
        );
        modelBuilder.Entity<PC>().HasData(
            new PC { Id = 1, Name = "Gaming Beast X", Weight = 12.5, Warranty = 36, CreatedAt = DateTime.Parse("2026-05-08T09:00:00"), Stock = 5 },
            new PC { Id = 2, Name = "Office Mini Pro", Weight = 4.2, Warranty = 24, CreatedAt = DateTime.Parse("2026-04-15T13:30:00"), Stock = 12 },
            new PC { Id = 3, Name = "Home Station", Weight = 8.0, Warranty = 24, CreatedAt = DateTime.Parse("2026-05-10T10:00:00"), Stock = 8 }
        );
        
        modelBuilder.Entity<PCComponent>().HasData(
            new PCComponent { PCId = 1, ComponentCode = "INT-I9-14X", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "NVD-RTX40X", Amount = 1 },
            new PCComponent { PCId = 2, ComponentCode = "AMD-R9-79X", Amount = 1 },
            new PCComponent { PCId = 3, ComponentCode = "NVD-RTX40X", Amount = 1 }
        );
    }
}