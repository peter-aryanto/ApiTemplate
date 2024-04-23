using Template1;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Template1Console1.Template1;

public class EntitiesHelper
{
    public static Context1 CreateContext1()
    {
        var context1 = new Context1(Context1Options);
        return context1;
    }

    public static Context1 CreateContext1FromPool()
    {
        var context1 = Context1Factory.CreateDbContext();
        return context1;
    }

    private static readonly string? Context1ConnString;
    private static readonly DbContextOptions<Context1> Context1Options;
    private static readonly PooledDbContextFactory<Context1> Context1Factory;

    static EntitiesHelper()
    {
        Context1ConnString = GetContext1ConnectionString();
        Context1Options = CreateContext1Options(Context1ConnString);
        Context1Factory = CreateContext1Factory(Context1Options);
    }

    private static string GetContext1ConnectionString()
    {
        // https://stackoverflow.com/questions/42268265/how-to-get-manage-user-secrets-in-a-net-core-console-application
        var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        var secretProvider = config.Providers.First();
        string? connString = null;
        secretProvider.TryGet("ConnectionStrings:Context1", out connString);
        return connString ?? throw new Exception("Cannot get connection string for Template1's Context1");
    }

    private static DbContextOptions<Context1> CreateContext1Options(string connString)
    {
        var context1Options = new DbContextOptionsBuilder<Context1>()
            .UseSqlServer(Context1ConnString)
            .Options;
        return context1Options;
    }

    private static PooledDbContextFactory<Context1> CreateContext1Factory(DbContextOptions<Context1> context1Options)
    {
        var context1Factory = new PooledDbContextFactory<Context1>(context1Options);
        return context1Factory;
    }
}
