using QuantityMeasurementApp.Entity;

namespace QuantityMeasurementApp.Repository;

public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
{
    private static QuantityMeasurementCacheRepository instance;

    private List<QuantityMeasurementEntity> cache =
        new List<QuantityMeasurementEntity>();

    private QuantityMeasurementCacheRepository() { }

    public static QuantityMeasurementCacheRepository GetInstance()
    {
        if (instance == null)
            instance = new QuantityMeasurementCacheRepository();

        return instance;
    }

    public void Save(QuantityMeasurementEntity entity)
    {
        cache.Add(entity);
    }

    public List<QuantityMeasurementEntity> GetAll()
    {
        return cache;
    }
}