using Microsoft.EntityFrameworkCore;
using RESTfull.Domain.Model;
using RESTfull.Infrastructure.Data.ModelConfiguration;

namespace RESTfull.Infrastructure;
public class Context : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Attending> Attendings { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserConfiguration().Configure(modelBuilder.Entity<User>());
        new ClassConfiguration().Configure(modelBuilder.Entity<Class>());
    }
}
