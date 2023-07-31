using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models;

[PrimaryKey(nameof(Id), nameof(ExternalId), nameof(Name))]
internal sealed class Show
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int? ExternalId { get; init; }
    public required string Name { get; init; }
    public string? Language { get; init; }
    public DateTime? Premiered { get; init; }
    public DateTime? Ended { get; init; }
    public string? Genres { get; init; }
    public string? Summary { get; init; }
    public required DateTime UpdatedAt { get; init; }
}
