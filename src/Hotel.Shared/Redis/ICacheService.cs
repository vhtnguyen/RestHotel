namespace Hotel.Shared.Redis;

public interface ICacheService
{
    Task<TData?> GetAsync<TData>(string key) where TData : class;
    Task<bool> DeleteAsync(string key);
    Task SetAsync<TData>(string key, TData data, DateTimeOffset ttl) where TData : class;
}
