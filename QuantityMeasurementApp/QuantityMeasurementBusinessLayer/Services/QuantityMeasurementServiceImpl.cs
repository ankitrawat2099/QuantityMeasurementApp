using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Repository;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Models;
using QuantityMeasurementModelLayer.Exceptions;
using System;
using System.Collections.Generic;

namespace QuantityMeasurementBusinessLayer.Services
{
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        private readonly QuantityMeasurementEFRepository _dbRepository;
        private readonly QuantityMeasurementCacheRepository _cacheRepository;
        private readonly QuantityService _quantityService;

        public QuantityMeasurementServiceImpl(
            QuantityMeasurementEFRepository dbRepository,
            QuantityMeasurementCacheRepository cacheRepository)
        {
            _dbRepository = dbRepository;
            _cacheRepository = cacheRepository;
            _quantityService = new QuantityService();
        }

        private void ClearCache()
        {
            _cacheRepository.ClearCache();
        }

       private void SaveOperation(QuantityMeasurementEntity entity)
{
    try
    {
        _dbRepository.SaveOperation(entity);
        ClearCache();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"DB FAILED - ADDING TO QUEUE: {ex.Message}");
        _cacheRepository.AddToQueue(entity);
    }
}

        // ---------------------- New Background Sync Method ----------------------
       
       
public void SyncQueueToDatabase()
{
    var queuedEntities = _cacheRepository.GetQueue();

    if (queuedEntities.Count == 0)
    {
        Console.WriteLine("QUEUE EMPTY");
        return;
    }

    Console.WriteLine($"SYNC STARTED: {queuedEntities.Count}");

    foreach (var entity in queuedEntities)
    {
        try
        {
            _dbRepository.SaveOperation(entity);
            Console.WriteLine($"SYNCED ID: {entity.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DB STILL OFFLINE: {ex.Message}");
            return; // ❗ STOP IMMEDIATELY
        }
    }

    // ✅ ONLY REACH HERE IF ALL SUCCESS
    _cacheRepository.ClearQueue();
    _cacheRepository.ClearCache();
}

        // ---------------------- Existing Methods ----------------------
        public double CompareQuantities(QuantityDTO thisQuantity, QuantityDTO thatQuantity)
        {
            if (thisQuantity.MeasurementType != thatQuantity.MeasurementType)
                throw new Exception("Cannot compare different measurement types");

            bool result;

            switch (thisQuantity.MeasurementType.ToUpper())
            {
                case "LENGTH":
                    var l1 = new QuantityModel<LengthUnit>(thisQuantity.Value, Enum.Parse<LengthUnit>(thisQuantity.Unit, true));
                    var l2 = new QuantityModel<LengthUnit>(thatQuantity.Value, Enum.Parse<LengthUnit>(thatQuantity.Unit, true));
                    result = _quantityService.Compare(l1, l2);
                    break;

                case "VOLUME":
                    var v1 = new QuantityModel<VolumeUnit>(thisQuantity.Value, Enum.Parse<VolumeUnit>(thisQuantity.Unit, true));
                    var v2 = new QuantityModel<VolumeUnit>(thatQuantity.Value, Enum.Parse<VolumeUnit>(thatQuantity.Unit, true));
                    result = _quantityService.Compare(v1, v2);
                    break;

                case "WEIGHT":
                    var w1 = new QuantityModel<WeightUnit>(thisQuantity.Value, Enum.Parse<WeightUnit>(thisQuantity.Unit, true));
                    var w2 = new QuantityModel<WeightUnit>(thatQuantity.Value, Enum.Parse<WeightUnit>(thatQuantity.Unit, true));
                    result = _quantityService.Compare(w1, w2);
                    break;

                case "TEMPERATURE":
                    var t1 = Enum.Parse<TemperatureUnit>(thisQuantity.Unit, true);
                    var t2 = Enum.Parse<TemperatureUnit>(thatQuantity.Unit, true);
                    double base1 = t1.ConvertToBaseUnit(thisQuantity.Value);
                    double base2 = t2.ConvertToBaseUnit(thatQuantity.Value);
                    result = Math.Abs(base1 - base2) < 0.0001;
                    break;

                default:
                    throw new Exception("Unsupported measurement type");
            }

            double compareResult = result ? 1 : 0;

            // Save operation (will go to DB or queue if offline)
            SaveOperation(new QuantityMeasurementEntity(
                thisQuantity.Value,
                thisQuantity.Unit,
                thatQuantity.Value,
                thatQuantity.Unit,
                "COMPARE",
                compareResult,
                thisQuantity.MeasurementType));

            return compareResult;
        }

        public QuantityDTO AddQuantities(QuantityDTO thisQuantity, QuantityDTO thatQuantity)
        {
            if (thisQuantity.MeasurementType != thatQuantity.MeasurementType)
                throw new Exception("Cannot perform operation on different measurement types");

            double result;
            string resultUnit = thisQuantity.Unit;

            switch (thisQuantity.MeasurementType.ToUpper())
            {
                case "LENGTH":
                    var l1 = new QuantityModel<LengthUnit>(thisQuantity.Value, Enum.Parse<LengthUnit>(thisQuantity.Unit, true));
                    var l2 = new QuantityModel<LengthUnit>(thatQuantity.Value, Enum.Parse<LengthUnit>(thatQuantity.Unit, true));
                    result = _quantityService.Add(l1, l2).Value;
                    break;

                case "VOLUME":
                    var v1 = new QuantityModel<VolumeUnit>(thisQuantity.Value, Enum.Parse<VolumeUnit>(thisQuantity.Unit, true));
                    var v2 = new QuantityModel<VolumeUnit>(thatQuantity.Value, Enum.Parse<VolumeUnit>(thatQuantity.Unit, true));
                    result = _quantityService.Add(v1, v2).Value;
                    break;

                case "WEIGHT":
                    var w1 = new QuantityModel<WeightUnit>(thisQuantity.Value, Enum.Parse<WeightUnit>(thisQuantity.Unit, true));
                    var w2 = new QuantityModel<WeightUnit>(thatQuantity.Value, Enum.Parse<WeightUnit>(thatQuantity.Unit, true));
                    result = _quantityService.Add(w1, w2).Value;
                    break;

                case "TEMPERATURE":
                    throw new UnsupportedOperationException("Addition not supported for Temperature");

                default:
                    throw new Exception("Unsupported measurement type");
            }

            var entity = new QuantityMeasurementEntity(
                thisQuantity.Value,
                thisQuantity.Unit,
                thatQuantity.Value,
                thatQuantity.Unit,
                "ADD",
                result,
                thisQuantity.MeasurementType);

            SaveOperation(entity);

            return new QuantityDTO(result, resultUnit, thisQuantity.MeasurementType);
        }



 public QuantityDTO SubtractQuantities(QuantityDTO thisQuantity, QuantityDTO thatQuantity)
        {
            if (thisQuantity.MeasurementType != thatQuantity.MeasurementType)
                throw new Exception("Cannot perform operation on different measurement types");

            double result;
            string resultUnit = thisQuantity.Unit;

            switch (thisQuantity.MeasurementType.ToUpper())
            {
                case "LENGTH":
                    var l1 = new QuantityModel<LengthUnit>(thisQuantity.Value, Enum.Parse<LengthUnit>(thisQuantity.Unit, true));
                    var l2 = new QuantityModel<LengthUnit>(thatQuantity.Value, Enum.Parse<LengthUnit>(thatQuantity.Unit, true));
                    result = _quantityService.Subtract(l1, l2).Value;
                    break;

                case "VOLUME":
                    var v1 = new QuantityModel<VolumeUnit>(thisQuantity.Value, Enum.Parse<VolumeUnit>(thisQuantity.Unit, true));
                    var v2 = new QuantityModel<VolumeUnit>(thatQuantity.Value, Enum.Parse<VolumeUnit>(thatQuantity.Unit, true));
                    result = _quantityService.Subtract(v1, v2).Value;
                    break;

                case "WEIGHT":
                    var w1 = new QuantityModel<WeightUnit>(thisQuantity.Value, Enum.Parse<WeightUnit>(thisQuantity.Unit, true));
                    var w2 = new QuantityModel<WeightUnit>(thatQuantity.Value, Enum.Parse<WeightUnit>(thatQuantity.Unit, true));
                    result = _quantityService.Subtract(w1, w2).Value;
                    break;

                case "TEMPERATURE":
                    throw new UnsupportedOperationException("Subtraction not supported for Temperature");

                default:
                    throw new Exception("Unsupported measurement type");
            }

            var entity = new QuantityMeasurementEntity(
                thisQuantity.Value,
                thisQuantity.Unit,
                thatQuantity.Value,
                thatQuantity.Unit,
                "SUBTRACT",
                result,
                thisQuantity.MeasurementType);

            SaveOperation(entity);

            return new QuantityDTO(result, resultUnit, thisQuantity.MeasurementType);
        }

        public QuantityDTO DivideQuantities(QuantityDTO thisQuantity, QuantityDTO thatQuantity)
        {
            if (thisQuantity.MeasurementType != thatQuantity.MeasurementType)
                throw new Exception("Cannot perform operation on different measurement types");

            double result;

            switch (thisQuantity.MeasurementType.ToUpper())
            {
                case "LENGTH":
                    var l1 = new QuantityModel<LengthUnit>(thisQuantity.Value, Enum.Parse<LengthUnit>(thisQuantity.Unit, true));
                    var l2 = new QuantityModel<LengthUnit>(thatQuantity.Value, Enum.Parse<LengthUnit>(thatQuantity.Unit, true));
                    result = _quantityService.Divide(l1, l2);
                    break;

                case "VOLUME":
                    var v1 = new QuantityModel<VolumeUnit>(thisQuantity.Value, Enum.Parse<VolumeUnit>(thisQuantity.Unit, true));
                    var v2 = new QuantityModel<VolumeUnit>(thatQuantity.Value, Enum.Parse<VolumeUnit>(thatQuantity.Unit, true));
                    result = _quantityService.Divide(v1, v2);
                    break;

                case "WEIGHT":
                    var w1 = new QuantityModel<WeightUnit>(thisQuantity.Value, Enum.Parse<WeightUnit>(thisQuantity.Unit, true));
                    var w2 = new QuantityModel<WeightUnit>(thatQuantity.Value, Enum.Parse<WeightUnit>(thatQuantity.Unit, true));
                    result = _quantityService.Divide(w1, w2);
                    break;

                case "TEMPERATURE":
                    throw new UnsupportedOperationException("Division not supported for Temperature");

                default:
                    throw new Exception("Unsupported measurement type");
            }

            var entity = new QuantityMeasurementEntity(
                thisQuantity.Value,
                thisQuantity.Unit,
                thatQuantity.Value,
                thatQuantity.Unit,
                "DIVIDE",
                result,
                thisQuantity.MeasurementType);

            SaveOperation(entity);

            return new QuantityDTO(result, thisQuantity.Unit, thisQuantity.MeasurementType);
        }



         public QuantityDTO ConvertQuantity(QuantityDTO quantity, string targetUnit)
        {
            double result;

            switch (quantity.MeasurementType.ToUpper())
            {
                case "LENGTH":
                    var sourceLength = new QuantityModel<LengthUnit>(quantity.Value, Enum.Parse<LengthUnit>(quantity.Unit, true));
                    LengthUnit targetLength = Enum.Parse<LengthUnit>(targetUnit, true);
                    result = targetLength.ConvertFromBaseUnit(sourceLength.Unit.ConvertToBaseUnit(sourceLength.Value));
                    break;

                case "VOLUME":
                    var sourceVolume = new QuantityModel<VolumeUnit>(quantity.Value, Enum.Parse<VolumeUnit>(quantity.Unit, true));
                    VolumeUnit targetVolume = Enum.Parse<VolumeUnit>(targetUnit, true);
                    result = targetVolume.ConvertFromBaseUnit(sourceVolume.Unit.ConvertToBaseUnit(sourceVolume.Value));
                    break;

                case "WEIGHT":
                    var sourceWeight = new QuantityModel<WeightUnit>(quantity.Value, Enum.Parse<WeightUnit>(quantity.Unit, true));
                    WeightUnit targetWeight = Enum.Parse<WeightUnit>(targetUnit, true);
                    result = targetWeight.ConvertFromBaseUnit(sourceWeight.Unit.ConvertToBaseUnit(sourceWeight.Value));
                    break;

                case "TEMPERATURE":
                    TemperatureUnit sourceTemp = Enum.Parse<TemperatureUnit>(quantity.Unit, true);
                    TemperatureUnit targetTemp = Enum.Parse<TemperatureUnit>(targetUnit, true);
                    result = targetTemp.ConvertFromBaseUnit(sourceTemp.ConvertToBaseUnit(quantity.Value));
                    break;

                default:
                    throw new Exception("Unsupported measurement type");
            }

            var entity = new QuantityMeasurementEntity(
                quantity.Value,
                quantity.Unit,
                0,
                targetUnit,
                "CONVERT",
                result,
                quantity.MeasurementType
            );

            SaveOperation(entity);

            return new QuantityDTO(result, targetUnit, quantity.MeasurementType);
        }


        // ---------------------- Cache-aware reads ----------------------
        public List<QuantityMeasurementEntity> GetErroredOperations()
        {
            var allData = _cacheRepository.GetCachedData() ?? _dbRepository.GetAll();
            return allData.FindAll(e => e.Operation.Contains("ERROR"));
        }

        public int GetOperationCount(string operationType)
        {
            var allData = _cacheRepository.GetCachedData() ?? _dbRepository.GetAll();
            return allData.FindAll(e => e.Operation == operationType).Count;
        }

        public (List<QuantityMeasurementEntity> data, string source) GetAllOperationsWithSource()
        {
            var cachedData = _cacheRepository.GetCachedData();
            if (cachedData != null)
                return (cachedData, "REDIS CACHE");

            var data = _dbRepository.GetAll();
            _cacheRepository.SetCache(data);
            return (data, "DATABASE");
        }
    }
}