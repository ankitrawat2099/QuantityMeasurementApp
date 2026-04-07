using QuantityMeasurementModelLayer.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace QuantityMeasurementRepositoryLayer.Repository
{
    public class QuantityMeasurementCacheRepository
    {
        private readonly IDistributedCache _cache;

        private const string QueueKey = "pending_operations_queue";

        public QuantityMeasurementCacheRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        // Generate User-specific cache key
        private string GetCacheKey(int userId)
        {
            return $"history:{userId}";
        }

        //user spcific cache

        public List<QuantityMeasurementEntity>? GetCachedData(int userId)
        {
            var cacheKey = GetCacheKey(userId);

            var cached = _cache.GetString(cacheKey);
            if (!string.IsNullOrEmpty(cached))
            {
                Console.WriteLine($"CACHE HIT (USER {userId}) - REDIS");
                return JsonSerializer.Deserialize<List<QuantityMeasurementEntity>>(cached);
            }

            Console.WriteLine($"CACHE MISS (USER {userId})");
            return null;
        }

        public void SetCache(int userId, List<QuantityMeasurementEntity> data)
        {
            var cacheKey = GetCacheKey(userId);

            var json = JsonSerializer.Serialize(data);

            _cache.SetString(cacheKey, json, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });

            Console.WriteLine($"CACHE SET FOR USER {userId}");
        }

        public void ClearCache(int userId)
        {
            var cacheKey = GetCacheKey(userId);

            _cache.Remove(cacheKey);
            Console.WriteLine($"CACHE CLEARED FOR USER {userId}");
        }

        //Queue

        public void AddToQueue(QuantityMeasurementEntity entity)
        {
            var json = JsonSerializer.Serialize(entity);

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

            Console.WriteLine($"ENTITY ADDED TO QUEUE - USER: {entity.UserId}");
        }

        public List<QuantityMeasurementEntity> GetQueue()
        {
            var queueData = _cache.GetString(QueueKey);
            if (string.IsNullOrEmpty(queueData))
                return new List<QuantityMeasurementEntity>();

            var queueList = JsonSerializer.Deserialize<List<string>>(queueData) ?? new List<string>();

            return queueList
                .Select(x => JsonSerializer.Deserialize<QuantityMeasurementEntity>(x)!)
                .ToList();
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

            Console.WriteLine($"ENTITY REMOVED FROM QUEUE - ID: {id}");
        }

        public void ClearQueue()
        {
            _cache.Remove(QueueKey);
            Console.WriteLine("QUEUE CLEARED");
        }
    }
}