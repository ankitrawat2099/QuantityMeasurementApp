using QuantityMeasurementApp.Entity;

namespace QuantityMeasurementApp.Repository;

public interface IQuantityMeasurementRepository
{
    void Save(QuantityMeasurementEntity entity);

    List<QuantityMeasurementEntity> GetAll();
}