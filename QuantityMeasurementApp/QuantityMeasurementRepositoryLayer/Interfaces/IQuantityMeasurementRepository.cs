using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementRepositoryLayer.Interfaces
{
    public interface IQuantityMeasurementRepository
    {
        void SaveOperation(QuantityMeasurementEntity entity);

        List<QuantityMeasurementEntity> GetAll();
    }
}