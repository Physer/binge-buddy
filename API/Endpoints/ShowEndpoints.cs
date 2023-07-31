using Application.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class ShowEndpoints
{
    public static IResult GetShows([FromServices] IRepository repository, [FromQuery] int limit = 100, [FromQuery] int offset = 0) => Results.Ok(repository.GetShows(limit, offset));
}
