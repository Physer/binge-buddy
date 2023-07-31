using Application.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class ShowEndpoints
{
    public static async Task<IResult> GetShowsAsync([FromServices] IDataReader dataReader, [FromQuery] int limit = 100, [FromQuery] int offset = 0) => Results.Ok(await dataReader.GetShowsAsync(limit, offset));
}
