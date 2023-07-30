using Application.Data;
using Domain;
using Microsoft.Extensions.Logging;

namespace Persistence;

internal class BingeRepository : IRepository
{
    private readonly BingeContext _dbContext;
    private readonly ILogger<BingeRepository> _logger;

    public BingeRepository(BingeContext dbContext,
        ILogger<BingeRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task AddOrUpdateShowsAsync(IEnumerable<Show> shows)
    {
        _logger.LogInformation("Mapping and inserting/updating {showsCount} into the database", shows.Count());
        var databaseModels = DatabaseMapper.Map(shows);
        _dbContext.Shows.UpdateRange(databaseModels);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Succesfully stored {showsCount} into the database", shows.Count());
    }
}
