namespace Persistence;

internal class DatabaseMapper
{
    public static IEnumerable<Models.Show> Map(IEnumerable<Domain.Show> domainModels)
        => domainModels.Select(Map);

    public static Models.Show Map(Domain.Show domainModel) => new()
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
