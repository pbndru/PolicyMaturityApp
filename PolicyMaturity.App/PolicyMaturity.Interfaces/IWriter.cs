using System.Collections.Generic;

namespace PolicyMaturity.Contracts
{
    public interface IWriter
    {
        void Write(List<PolicyDetail> PolicyDetail);
    }
}
