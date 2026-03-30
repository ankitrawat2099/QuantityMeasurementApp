using QuantityMeasurementModelLayer.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace QuantityMeasurementRepositoryLayer.Repository
{
    public class QuantityMeasurementCacheRepository
    {
        private readonly IDistributedCache _cache;
        private const string CacheKey = "all_operations";

        private const string QueueKey = "pending_operations_queue"; 
        public QuantityMeasurementCacheRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        // ----------------- Existing Cache Methods -----------------
        public List<QuantityMeasurementEntity>? GetCachedData()
        {
            var cached = _cache.GetString(CacheKey);
            if (!string.IsNullOrEmpty(cached))
            {
                Console.WriteLine("CACHE HIT - DATA FROM REDIS");
                return JsonSerializer.Deserialize<List<QuantityMeasurementEntity>>(cached);
            }

            Console.WriteLine("CACHE MISS - NO DATA IN REDIS");
            return null;
        }

        public void SetCache(List<QuantityMeasurementEntity> data)
        {
            var json = JsonSerializer.Serialize(data);
            _cache.SetString(CacheKey, json, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });
            Console.WriteLine("DATA STORED IN REDIS CACHE");
        }

        public void ClearCache()
        {
            _cache.Remove(CacheKey);
            Console.WriteLine("CACHE CLEARED");
        }

        // ----------------- Queue Methods for Disconnected Architecture -----------------
        public void AddToQueue(QuantityMeasurementEntity entity)
        {
            var json = JsonSerializer.Serialize(entity);
            // Store in Redis list (append)
            var existingQueue = _cache.GetString(QueueKey);
            List<string> queueList;
            if (!string.IsNullOrEmpty(existingQueue))
            {
                queueList = JsonSerializer.Deserialize<List<string>>(existingQueue) ?? new List<string>();
            }
            else
            {
                queueList = new List<string>();
            }

            queueList.Add(json);
            _cache.SetString(QueueKey, JsonSerializer.Serialize(queueList));
            Console.WriteLine($"ENTITY ADDED TO REDIS QUEUE - ID: {entity.Id}");
        }

        public List<QuantityMeasurementEntity> GetQueue()
        {
            var queueData = _cache.GetString(QueueKey);
            if (string.IsNullOrEmpty(queueData)) return new List<QuantityMeasurementEntity>();

            var queueList = JsonSerializer.Deserialize<List<string>>(queueData) ?? new List<string>();
            return queueList.Select(x => JsonSerializer.Deserialize<QuantityMeasurementEntity>(x)!).ToList();
        }

       public void RemoveFromQueue(int id)
{
    var queueData = _cache.GetString(QueueKey);
    if (string.IsNullOrEmpty(queueData)) return;

    var queueList = JsonSerializer.Deserialize<List<string>>(queueData) ?? new List<string>();
    queueList = queueList
        .Where(x => JsonSerializer.Deserialize<QuantityMeasurementEntity>(x)!.Id != id)
        .ToList();

    _cache.SetString(QueueKey, JsonSerializer.Serialize(queueList));
    Console.WriteLine($"ENTITY REMOVED FROM REDIS QUEUE - ID: {id}");
}


public void ClearQueue()
{
    _cache.Remove("pending_operations_queue");
    Console.WriteLine("QUEUE CLEARED FROM REDIS");
}
    }
}