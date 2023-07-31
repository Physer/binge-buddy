using Domain;

namespace Application.Data;

public interface IDataReader
{
    Task<IEnumerable<Show>> GetShowsAsync(int limit, int offset);
}
