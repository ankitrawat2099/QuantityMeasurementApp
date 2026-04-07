using Microsoft.EntityFrameworkCore;
using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementRepositoryLayer.Context;

public class QuantityMeasurementDbContext : DbContext
{
    public QuantityMeasurementDbContext(DbContextOptions<QuantityMeasurementDbContext> options)
        : base(options)
    {
    }

    public DbSet<QuantityMeasurementEntity> QuantityMeasurements { get; set; }
    public DbSet<UserEntity> Users { get; set; }

}