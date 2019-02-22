using System;

namespace PolicyMaturity.Contracts
{
    public class PolicyManager: IPolicyManager
    {
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly ICalculator calculator;
        private readonly IValidator validator;

        public PolicyManager(IWriter writer, IReader reader, ICalculator calculator, IValidator validator)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (calculator == null)
                throw new ArgumentNullException("calculator");
            if (validator == null)
                throw new ArgumentNullException("validator");

            this.writer = writer;
            this.reader = reader;
            this.calculator = calculator;
            this.validator = validator;
        }

        public bool Execute(string path, bool hasHeaders)
        {
            this.validator.Validate(path);

            var policies = this.reader.Read(path, hasHeaders);

            if (policies?.Count > 0)
            {
                var maturityPolicies = this.calculator.CalculateMaturity(policies);
            
                this.writer.Write(maturityPolicies);

                return true;
            }

            return false;
        }
    }
}
