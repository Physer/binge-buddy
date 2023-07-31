using Domain;

namespace Application.Data;

public interface IRepository
{
    void AddOrUpdateShows(IEnumerable<Show> shows);
    IEnumerable<Show> GetShows(int limit, int offset);
}
