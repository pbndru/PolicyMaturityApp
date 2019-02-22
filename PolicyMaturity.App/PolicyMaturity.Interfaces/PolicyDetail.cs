using System;

namespace PolicyMaturity.Contracts
{
    public class PolicyDetail
    {
        public string PolicyNumber { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public decimal Premiums { get; set; }
        public bool Membership { get; set; }
        public decimal DiscretionaryBonus { get; set; }
        public decimal UpliftPercentage { get; set; }
        public decimal MaturityValue { get; set; }
    }
}
