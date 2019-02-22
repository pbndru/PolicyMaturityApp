using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using PolicyMaturity.Contracts;

namespace PolicyMaturity.BusinessLayer
{
   public class CsvReader : IReader
    {
        public List<PolicyDetail> Read(string path, bool hasHeaders)
        {
            var policyDetails = new List<PolicyDetail>();

            foreach (var row in File.ReadLines(path).Skip(hasHeaders ? 1 : 0))
            {
                policyDetails.Add(ReadPolicyRows(row));
            }

            return policyDetails;
        }

        private PolicyDetail ReadPolicyRows(string row)
        {
            var data = row.Split(',');
            return new PolicyDetail()
                       {
                           PolicyNumber = data[0],
                           PolicyStartDate = DateTime.Parse(data[1]),
                           Premiums = decimal.Parse(data[2]),
                           Membership = Helper.BooleanAliasesHelper(data[3]),
                           DiscretionaryBonus = decimal.Parse(data[4]),
                           UpliftPercentage = decimal.Parse(data[5])
                       };
        }
    }
}
