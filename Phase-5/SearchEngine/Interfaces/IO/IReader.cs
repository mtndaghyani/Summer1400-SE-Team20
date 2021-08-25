using System.Collections.Generic;

namespace SearchEngine.Interfaces.IO
{
    public interface IReader
    {
        IEnumerable<string> Read();
    }
}