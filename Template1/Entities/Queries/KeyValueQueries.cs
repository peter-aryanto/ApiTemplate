using Microsoft.EntityFrameworkCore;

namespace Template1.Entities.Queries;

public interface IKeyValueQueries
{
    //qwe6
    Task<KeyValue> CreateAsync(string key, string val1, string val2);
    Task<List<KeyValue>> GetAsync();
}

public class KeyValueQueries : IKeyValueQueries
{
    private readonly Context1 context;

    public KeyValueQueries(Context1 context)
    {
        this.context = context;
    }

    public async Task<KeyValue> CreateAsync(string key, string val1, string val2)
    {
        // throw new NotImplementedException();
        //qwe8
        var rec = new KeyValue
        {
            Key = key,
            Value1 = val1,
            Value2 = val2,
        };
        // context.KeyValues.Add(rec);
        await context.SaveChangesAsync();
        return rec;
        // return null;
    }

    public async Task<List<KeyValue>> GetAsync()
    {
        var output = await context.KeyValues
            .ToListAsync();
        return output;
    }
}