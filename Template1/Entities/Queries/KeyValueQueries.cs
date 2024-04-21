using Microsoft.EntityFrameworkCore;

namespace Template1.Entities.Queries;

public interface IKeyValueQueries
{
    Task<List<KeyValue>> GetAsync();
}

public class KeyValueQueries : IKeyValueQueries
{
    private readonly Context1 context;

    public KeyValueQueries(Context1 context)
    {
        this.context = context;
    }

    public async Task<List<KeyValue>> GetAsync()
    {
        var output = await context.KeyValues
            .ToListAsync();
        return output;
    }
}