using Template1Console1.Utils;

namespace Template1Console1;

public interface IProcess01
{
    bool DoProcess();
}

public class Process01 : IProcess01
{
    public bool DoProcess()
    {
        return true;
    }
}