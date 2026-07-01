public interface IRedisService
{
    Task<long> IncrementAsync(string key);

    Task<long> GetLongAsync(string key);
}