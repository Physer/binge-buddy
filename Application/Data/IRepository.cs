using Domain;

namespace Application.Data;

public interface IRepository
{
    Task AddOrUpdateShowsAsync(IEnumerable<Show> shows);
}
