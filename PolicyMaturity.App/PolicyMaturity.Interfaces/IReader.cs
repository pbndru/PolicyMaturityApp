using System.Collections.Generic;

namespace PolicyMaturity.Contracts
{
    public interface IReader
    {
        List<PolicyDetail> Read(string path, bool hasHeaders);
    }
}
