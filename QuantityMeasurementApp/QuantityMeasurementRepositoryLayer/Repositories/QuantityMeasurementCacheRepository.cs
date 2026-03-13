using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementRepositoryLayer.Repositories;

public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
{
    private readonly List<QuantityMeasurementEntity> cache = new();

    public void Save(QuantityMeasurementEntity entity)
    {
        cache.Add(entity);
    }

    public List<QuantityMeasurementEntity> GetAll()
    {
        return cache;
    }

    public List<QuantityMeasurementEntity> GetByOperation(string operation)
    {
        List<QuantityMeasurementEntity> result = new List<QuantityMeasurementEntity>();

        foreach (var measurement in cache)
        {
            if (measurement.Operation.Equals(operation, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(measurement);
            }
        }

        return result;
    }

    public List<QuantityMeasurementEntity> GetByMeasurementType(string measurementType)
    {
        List<QuantityMeasurementEntity> result = new List<QuantityMeasurementEntity>();

        foreach (var measurement in cache)
        {
            if (measurement.MeasurementType.Equals(measurementType, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(measurement);
            }
        }

        return result;
    }

    public int GetTotalCount()
    {
        return cache.Count;
    }

    public void DeleteAll()
    {
        cache.Clear();
    }
}