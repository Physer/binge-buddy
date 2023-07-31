namespace Persistence;

internal class DatabaseMapper
{
    public static IEnumerable<Domain.Show> Map(IEnumerable<Models.Show> databaseModels)
        => databaseModels.Select(Map);

    private static Domain.Show Map(Models.Show databaseModel) => new()
    {
        ExternalId = databaseModel.ExternalId,
        Ended = databaseModel.Ended.HasValue ? new DateOnly(databaseModel.Ended.Value.Year, databaseModel.Ended.Value.Month, databaseModel.Ended.Value.Day) : null,
        Genres = databaseModel.Genres?.Split(",") ?? Enumerable.Empty<string>(),
        Language = databaseModel.Language,
        Name = databaseModel.Name,
        Premiered = databaseModel.Premiered.HasValue ? new DateOnly(databaseModel.Premiered.Value.Year, databaseModel.Premiered.Value.Month, databaseModel.Premiered.Value.Day) : null,
        Summary = databaseModel.Summary
    };

    public static IEnumerable<Models.Show> Map(IEnumerable<Domain.Show> domainModels)
        => domainModels.Select(Map);

    private static Models.Show Map(Domain.Show domainModel) => new()
    {
        ExternalId = domainModel.ExternalId,
        Language = domainModel.Language,
        Name = domainModel.Name,
        Premiered = domainModel.Premiered?.ToDateTime(TimeOnly.MinValue),
        Ended = domainModel.Ended?.ToDateTime(TimeOnly.MinValue),
        Genres = string.Join(",", domainModel.Genres),
        Summary = domainModel.Summary,
        UpdatedAt = DateTime.UtcNow
    };
}
