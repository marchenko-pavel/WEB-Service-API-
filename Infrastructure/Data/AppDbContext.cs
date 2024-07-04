﻿using Infrastructure.Data.EfModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    static AppDbContext() { AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<ConsumptionObject> ConsumptionObjects { get; set; }
    public DbSet<DeliveryPoint> DeliveryPoints { get; set; }
    public DbSet<CalculationMeter> CalculationMeters { get; set; }
    public DbSet<MeasuringPoint> MeasuringPoints { get; set; }
    public DbSet<ElectricMeter> ElectricMeters { get; set; }
    public DbSet<ElectricMeterType> ElectricMeterTypes { get; set; }
    public DbSet<CurrentTransformer> CurrentTransformers { get; set; }
    public DbSet<CurrentTransformerType> CurrentTransformerTypes { get; set; }
    public DbSet<VoltageTransformer> VoltageTransformers { get; set; }
    public DbSet<VoltageTransformerType> VoltageTransformerTypes { get; set; }
    public DbSet<CalculationMeterPlugIn> CalculationMeterPlugIns { get; set; }
    protected override async void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ElectricMeter>().HasOne(x => x.MeasuringPoint).WithOne(x => x.ElectricMeter)
            .HasForeignKey<ElectricMeter>(x => x.Id).IsRequired();
        modelBuilder.Entity<VoltageTransformer>().HasOne(x => x.MeasuringPoint).WithOne(x => x.VoltageTransformer)
            .HasForeignKey<VoltageTransformer>(x => x.Id).IsRequired();
        modelBuilder.Entity<CurrentTransformer>().HasOne(x => x.MeasuringPoint).WithOne(x => x.CurrentTransformer)
            .HasForeignKey<CurrentTransformer>(x => x.Id).IsRequired();
        modelBuilder.Entity<CalculationMeter>().HasOne(x => x.DeliveryPoint).WithOne(x => x.CalculationMeter)
            .HasForeignKey<CalculationMeter>(x => x.Id).IsRequired();
        modelBuilder.Entity<CalculationMeter>().HasMany(x => x.MeasuringPoints).WithMany(y => y.CalculationMeters)
            .UsingEntity<CalculationMeterPlugIn>();
        modelBuilder.Entity<CalculationMeterPlugIn>()
            .HasKey(x => new { x.MeasuringPointId, x.CalculationMeterId, x.PlugedIn });
        modelBuilder.Entity<Organization>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<ConsumptionObject>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<DeliveryPoint>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<MeasuringPoint>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<ElectricMeterType>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<CurrentTransformerType>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<VoltageTransformerType>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<ElectricMeter>().HasIndex(x => x.InventoryNumber).IsUnique();
        modelBuilder.Entity<CurrentTransformer>().HasIndex(x => x.InventoryNumber).IsUnique();
        modelBuilder.Entity<VoltageTransformer>().HasIndex(x => x.InventoryNumber).IsUnique();
    }
}
