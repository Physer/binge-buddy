using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence;

public class BingeContext : DbContext
{
    public BingeContext(DbContextOptions options) : base(options) { }

    public DbSet<Show> Shows { get; set; }
}
