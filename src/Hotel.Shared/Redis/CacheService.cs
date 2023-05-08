
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Hotel.Shared.Redis;

internal class CacheService : ICacheService
{
    private readonly IDatabase _database;
    public CacheService(
        IConnectionMultiplexer connection)
    {
        _database = connection.GetDatabase();
    }
    public async Task<bool> DeleteAsync(string key)
    {
        var isExist = await _database.KeyExistsAsync(key);
        if (isExist == false)
        {
            return false;
        }
        await _database.KeyDeleteAsync(key);
        return true;
    }

    public async Task<TData?> GetAsync<TData>(string key) where TData : class
    {
        var data = await _database.StringGetAsync(key);
        if(key == null)
        {
            return default;
        }
        var result = JsonConvert.DeserializeObject<TData>(data!);
        return result;
    }

    public async Task SetAsync<TData>(string key, TData data, DateTimeOffset ttl) where TData : class
    {
        var serializeData = JsonConvert.SerializeObject(data);
        var expirationTime = ttl.DateTime.Subtract(DateTime.Now);
        await _database.StringSetAsync(key, serializeData, expirationTime);
    }
}
