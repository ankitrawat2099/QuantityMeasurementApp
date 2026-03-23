using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Exceptions;
using System.Collections.Generic;

namespace QuantityMeasurementBusinessLayer.Interfaces
{
    public interface IQuantityMeasurementService
    {
        double CompareQuantities(QuantityDTO thisQuantity, QuantityDTO thatQuantity);
        QuantityDTO AddQuantities(QuantityDTO thisQuantity, QuantityDTO thatQuantity);
         QuantityDTO SubtractQuantities(QuantityDTO thisQuantity, QuantityDTO thatQuantity);
        QuantityDTO DivideQuantities(QuantityDTO thisQuantity, QuantityDTO thatQuantity);
        QuantityDTO ConvertQuantity(QuantityDTO quantity, string targetUnit);

        List<QuantityMeasurementEntity> GetErroredOperations();
        int GetOperationCount(string operationType);
        void SyncQueueToDatabase();
        (List<QuantityMeasurementEntity> data, string source) GetAllOperationsWithSource();
    }
}