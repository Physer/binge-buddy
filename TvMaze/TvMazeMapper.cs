using Domain;
using TvMaze.Models;

namespace TvMaze;

internal class TvMazeMapper
{
    public static IEnumerable<Show> Map(IEnumerable<TvMazeShow> externalShowModels)
        => externalShowModels.Select(Map);

    public static Show Map(TvMazeShow externalShowModel)
        => new(externalShowModel.Name, externalShowModel.Language, externalShowModel.Premiered, externalShowModel.Ended, externalShowModel.Genres, externalShowModel.Summary);
}
