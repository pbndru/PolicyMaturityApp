using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolicyMaturity.Contracts;
using PolicyMaturity.BusinessLayer;
using System.Collections.Generic;
using System.IO;


//   WE CAN USE FLUENT ASSERTIONS HERE FOR READABILITY 
//   WE CAN WRITE MORE TESTS:      
//   TO CHECK ROW BASED ON 3 POLICY TYPES 
//   EDGE CONDITIONS IF FILE ROWS HAVE DODGY DATA
//   INPUTS AND OUTPUTS


namespace PolicyMaturity.Tests
{
    [TestClass]
    public class PolicyMaturityTests
    {
        private Mock<IReader> reader;
        private Mock<IWriter> writer;
        private Mock<ICalculator> calculator;
        private Mock<IValidator> validator;

        private readonly string path = "MaturityDataTest.csv";
        private readonly string incorrectPath = "FileNotExists.csv";
        private readonly string incorrectExtension = "Extension.xml";


        [TestInitialize]
        public void TestSetup()
        {
            this.reader = new Mock<IReader>();
            this.writer = new Mock<IWriter>();
            this.calculator = new Mock<ICalculator>();
            this.validator = new Mock<IValidator>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowsArgumentNullExceptionWhenWriterIsNull()
        {
            new PolicyManager(null, this.reader.Object, this.calculator.Object, this.validator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowsArgumentNullExceptionWhenReaderIsNull()
        {
            new PolicyManager(this.writer.Object, null, this.calculator.Object, this.validator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowsArgumentNullExceptionWhenCalculatorIsNull()
        {
            new PolicyManager(this.writer.Object, this.reader.Object, null, this.validator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowsArgumentNullExceptionWhenValidatorIsNull()
        {
            new PolicyManager(this.writer.Object, this.reader.Object, this.calculator.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void PolicyManagerExecuteThrowsFileNotFoundExceptionWhenCsvFileNotExists()
        {
            var validator = new CsvValidator();

            var policyManager = new PolicyManager(
                this.writer.Object,
                this.reader.Object,
                this.calculator.Object,
                validator);

            policyManager.Execute(this.incorrectPath, true);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void PolicyManagerExecuteThrowsFileNotFoundExceptionWhenExtensionIsNotCsv()
        {
            var validator = new CsvValidator();

            var policyManager = new PolicyManager(
                this.writer.Object,
                this.reader.Object,
                this.calculator.Object,
                validator);

            policyManager.Execute(this.incorrectExtension, true);
        }

        [TestMethod]
        public void PolicyManagerExecuteWhenPolicyDetailsExistsThenWriteToXml()
        {
            var calculator = new XmlCalculator();
            var policyDetails = this.PolicyDetail();

            var policyManager = new PolicyManager(
             this.writer.Object,
             this.reader.Object,
             calculator,
             this.validator.Object);

            this.reader.Setup(x => x.Read(this.path, true)).Returns(policyDetails);

            policyManager.Execute(this.path, true);
            
            this.writer.Verify(x => x.Write(policyDetails), Times.Once);
        }

        [TestMethod]
        public void PolicyManagerExecuteWhenPolicyDetailsAreNullThenNotWriteToXml()
        {
            var policyDetails = new List<PolicyDetail>();
            var policyManager = this.PolicyManager();
            
            policyManager.Execute(this.path, true);
            
            this.writer.Verify(x => x.Write(policyDetails), Times.Never);
        }

        [TestMethod]
        public void PolicyManagerExecuteWhenPolicyDetailsCountIsZeroThenNotWriteToXml()
        {
            var policyDetails = new List<PolicyDetail>();
            var policyManager = this.PolicyManager();

            // Arrange
            this.reader.Setup(x => x.Read(this.path, true)).Returns(policyDetails);

            // Act
            policyManager.Execute(this.path, true);

            // Assert
            this.writer.Verify(x => x.Write(policyDetails), Times.Never);
        }

        private List<PolicyDetail> PolicyDetail()
        {
            var policyDetails = new List<PolicyDetail>();
            var policyDetail = new PolicyDetail
                       {
                           PolicyNumber = "A111",
                           PolicyStartDate = DateTime.Now,
                           Premiums = 1000,
                           Membership = false,
                           DiscretionaryBonus = 3000,
                           UpliftPercentage = 2000,
                           MaturityValue = 0
                       };

            policyDetails.Add(policyDetail);
            return policyDetails;
        }

        private PolicyManager PolicyManager()
        {
            return new PolicyManager(
                this.writer.Object, 
                this.reader.Object, 
                this.calculator.Object, 
                this.validator.Object);
        }
    }
}
