using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using PolicyMaturity.Contracts;

namespace PolicyMaturity.BusinessLayer
{
    public class XmlWriter : IWriter
    {
        public void Write(List<PolicyDetail> policies)
        {
            XDocument xdoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Policies",
                from policy in policies
                select
                  new XElement("Policy", new XAttribute("StartDate", policy.PolicyStartDate),
                  new XElement("PolicyNumber", policy.PolicyNumber),
                  new XElement("MaturityAmount", policy.MaturityValue.ToString("C")))));

            xdoc.Save(System.AppDomain.CurrentDomain.BaseDirectory + "//..//..//output.xml");
        }
    }
}
