using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PaySlipGenerator.Calculator;
using PaySlipGenerator.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaySlipGenerator.Test.Unit.Calculator
{
    [TestClass]
    public class IncomeTaxCalculatorTest
    {
        private IncomeTaxCalculator _subject;
        private readonly Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
        private readonly Mock<ILogger<IncomeTaxCalculator>> _logger = new Mock<ILogger<IncomeTaxCalculator>>();
        private readonly Mock<IIncomeTaxStrategy> _incomeTaxStrategy = new Mock<IIncomeTaxStrategy>();

        [TestInitialize]
        public void SetUp()
        {
            _subject = new IncomeTaxCalculator(_logger.Object, _incomeTaxStrategy.Object, _configuration.Object);
            SetConfigurationValues();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_NegativeSalary_ThrowsArgumentException()
        {
            //Arrange
            var annualSalary = -10;
            //Act
            var actual = _subject.Calculate(annualSalary);
            //Assert
        }

        [TestMethod]
        public void Calculate_Tier0Salary_IdentifyTier0TaxThreshold()
        {
            //Arrange
            var annualSalary = 15000;
            var expected = 0;
            
            _incomeTaxStrategy.Setup(x => x.Calculate(annualSalary, TaxThreshold.Tier0))
                .Returns(expected);

            //Act
            var actual = _subject.Calculate(annualSalary);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        private void SetConfigurationValues()
        {
            _configuration.Setup(a => a.GetSection(Constants.IncomeThresholdTier0))
                .Returns(CreateMockIConfigurationSection("18201").Object);
            _configuration.Setup(a => a.GetSection(Constants.IncomeThresholdTier1))
                .Returns(CreateMockIConfigurationSection("37001").Object);
            _configuration.Setup(a => a.GetSection(Constants.IncomeThresholdTier2))
                .Returns(CreateMockIConfigurationSection("87001").Object);
            _configuration.Setup(a => a.GetSection(Constants.IncomeThresholdTier3))
                .Returns(CreateMockIConfigurationSection("180001").Object);
            _configuration.Setup(a => a.GetSection(Constants.IncomeThresholdTier4))
                .Returns(CreateMockIConfigurationSection("180000").Object);
        }

        private Mock<IConfigurationSection> CreateMockIConfigurationSection(string value)
        {
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value)
                .Returns(value);
            return configurationSection;
        }

        [TestMethod]
        public void Calculate_Tier1Salary_IdentifyTier1TaxThreshold()
        {
            //Arrange
            var annualSalary = 20000;
            var expected = 28;
            
            _incomeTaxStrategy.Setup(x => x.Calculate(annualSalary, TaxThreshold.Tier1))
                .Returns(expected);

            //Act
            var actual = _subject.Calculate(annualSalary);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_Tier2Salary_IdentifyTier2TaxThreshold()
        {
            //Arrange
            var annualSalary = 60050;
            var expected = 922;
            
            _incomeTaxStrategy.Setup(x => x.Calculate(annualSalary, TaxThreshold.Tier2))
                .Returns(expected);

            //Act
            var actual = _subject.Calculate(annualSalary);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_Tier3Salary_IdentifyTier3TaxThreshold()
        {
            //Arrange
            var annualSalary = 120000;
            var expected = 2669;

            _incomeTaxStrategy.Setup(x => x.Calculate(annualSalary, TaxThreshold.Tier3))
                .Returns(expected);

            //Act
            var actual = _subject.Calculate(annualSalary);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_Tier4Salary_IdentifyTier4TaxThreshold()
        {
            //Arrange
            var annualSalary = 200000;
            var expected = 5269;

            _incomeTaxStrategy.Setup(x => x.Calculate(annualSalary, TaxThreshold.Tier4))
                .Returns(expected);

            //Act
            var actual = _subject.Calculate(annualSalary);
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
