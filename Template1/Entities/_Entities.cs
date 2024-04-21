using Microsoft.EntityFrameworkCore;
using Template1.Entities;


namespace Template1
{
  public class Context1 : DbContext
  {
    // private readonly ICurrentUserService _currentUserService;
    // private readonly IDateTime _dateTime;

    // internal static readonly DbContextOptions<Context1> Options = new();
    // public Context1() : this(Options)
    // {}

    public Context1(DbContextOptions<Context1> options) : base(options)
    {}

    public Context1() : base() // Parameterless constructor for EF Core Mock in unit tests.
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

    public virtual DbSet<KeyValue> KeyValues { get; set; }

    public virtual DbSet<AdditionalInfo> AdditionalInfos { get; set; }

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
    // private record ConnStrings(string Context1);

    public static IServiceCollection AddContext1(this IServiceCollection services, ConfigurationManager configManager)
    {
      // var configManager = builder.Configuration;
      // var connStrings = configManager.GetSection("ConnectionStrings").Get<ConnStrings>();
      // var connString = connStrings.Context1
      var connString = configManager["ConnectionStrings:Context1"];
      Console.WriteLine($"|Console: {connString}|{Environment.NewLine}=======");
      services.AddDbContext<Context1>(options =>
          // options.UseSqlServer()
          options.UseSqlServer(connString)
        );
      return services;
    }

  }
}
