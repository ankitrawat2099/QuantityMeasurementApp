using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Context;
using QuantityMeasurementRepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace QuantityMeasurementRepositoryLayer.Repository
{
    public class QuantityMeasurementEFRepository : IQuantityMeasurementRepository
    {
        private readonly QuantityMeasurementDbContext _context;

        public QuantityMeasurementEFRepository(
            QuantityMeasurementDbContext context, 
            QuantityMeasurementCacheRepository cacheRepository)
        {
            _context = context;
        }

        
            public void SaveOperation(QuantityMeasurementEntity entity)
{
    _context.QuantityMeasurements.Add(entity);
    _context.SaveChanges(); // let it throw exception
}
        public List<QuantityMeasurementEntity> GetAll()
        {
            return _context.QuantityMeasurements.ToList();
        }
    }
}