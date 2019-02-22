using System.Collections.Generic;

namespace PolicyMaturity.Contracts
{
    public interface ICalculator
    {
        List<PolicyDetail> CalculateMaturity(List<PolicyDetail> PolicyDetails);
    }
}
