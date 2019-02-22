using System;
using System.Collections.Generic;
using PolicyMaturity.Contracts;

namespace PolicyMaturity.BusinessLayer
{
    public class XmlCalculator: ICalculator
    {
        public List<PolicyDetail> CalculateMaturity(List<PolicyDetail> PolicyDetails)
        {
            PolicyDetails.ForEach(x => x.MaturityValue = this.CalculateMaturityValue(x));

            return PolicyDetails;
        }

        private decimal CalculateMaturityValue(PolicyDetail PolicyDetail)
        {
            decimal managementFee = this.CalculateManagementFeePercentage(PolicyDetail);
            var maturityValue = ((PolicyDetail.Premiums - (PolicyDetail.Premiums * managementFee / 100)) + PolicyDetail.DiscretionaryBonus) * PolicyDetail.UpliftPercentage / 100;

            return maturityValue;
        }

        private decimal CalculateManagementFeePercentage(PolicyDetail PolicyDetail)
        {
            decimal result = 0.0m;
            var minDate = new DateTime(1990, 1, 1);

            if (PolicyDetail.PolicyStartDate <= minDate)
                result = 3.0m;

            if (PolicyDetail.PolicyStartDate >= minDate && PolicyDetail.Membership)
                result = 7.00m;

            if (PolicyDetail.PolicyStartDate <= minDate && PolicyDetail.Membership)
                result = 5.00m;

            return result;
        }
    }
}
