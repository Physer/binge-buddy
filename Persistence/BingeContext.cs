using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence;

internal class BingeContext : DbContext
{
    public BingeContext(DbContextOptions options) : base(options) { }

    public DbSet<Show> Shows { get; set; }
}
