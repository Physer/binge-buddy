namespace Application.Data;

public interface ICacher
{
    Task<T?> GetAsync<T>(string cacheKey);
    void Store<T>(string cacheKey, T input);
}
