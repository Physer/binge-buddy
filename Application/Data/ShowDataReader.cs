using Domain;

namespace Application.Data;

public class ShowDataReader : IDataReader
{
    private readonly ICacher _cacher;
    private readonly IRepository _repository;

    public ShowDataReader(ICacher cacher,
        IRepository repository)
    {
        _cacher = cacher;
        _repository = repository;
    }

    public async Task<IEnumerable<Show>> GetShowsAsync(int limit, int offset)
    {
        var cacheKey = $"shows-{limit}-{offset}";
        var cachedResult = await _cacher.GetAsync<IEnumerable<Show>>(cacheKey);
        if (cachedResult is not null)
            return cachedResult;

        var shows = _repository.GetShows(limit, offset);
        _cacher.Store(cacheKey, shows);
        return shows;
    }
}
