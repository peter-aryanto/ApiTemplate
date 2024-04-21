using Microsoft.EntityFrameworkCore;
using Template1.Entities;


namespace Template1
{
  public class Context1 : DbContext
  {
    // private readonly ICurrentUserService _currentUserService;
    // private readonly IDateTime _dateTime;

    public Context1(DbContextOptions<Context1> options) : base(options)
    {}

    /*
    public Context1(
      DbContextOptions<Context1> options,
      ICurrentUserService currentUserService,
      DateTime dateTime)
      : base(options)
    {
      _currentUserService = currentUserService;
      _dateTime = dateTime;
    }
    */

    public DbSet<KeyValue> KeyValues { get; set; }

    public DbSet<AdditionalInfo> AdditionalInfos { get; set; }

    /*
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
      {
        switch (entry.State)
        {
          case EntityState.Added:
            entry.Entity.CreatedBy = _currentUserService.UserId;
            entry.Entity.Created = _dateTime.Now;
            break;
          case EntityState.Modified:
            entry.Entity.LastModifiedBy = _currentUserService.UserId;
            entry.Entity.LastModified = _dateTime.Now;
            break;
        }
      }

      return base.SaveChangesAsync(cancellationToken);
    }
    */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context1).Assembly);
    }
  }

  public static class Context1Service
  {
    public static IServiceCollection AddContext1(this IServiceCollection services/*, IConfiguration config*/)
    {
      services.AddDbContext<Context1>(options =>
          options.UseSqlServer()
          // options.UseSqlServer("Server=10.252.150.206;Database=dbMigration;User Id=sa;Password=5SNMsmZrqgvTso8OUpJ2;Encrypt=true;trustServerCertificate=true;")
        );
      return services;
    }

  }
}
