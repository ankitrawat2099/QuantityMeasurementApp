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
            QuantityMeasurementDbContext context)
        {
            _context = context;
        }

        
            public void SaveOperation(QuantityMeasurementEntity entity)
{
    _context.QuantityMeasurements.Add(entity);
    _context.SaveChanges();
}
        public List<QuantityMeasurementEntity> GetAll()
        {
            return _context.QuantityMeasurements.ToList();
        }
    }
}