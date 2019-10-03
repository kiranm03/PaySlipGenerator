using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PaySlipGenerator.Calculator;
using PaySlipGenerator.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PaySlipGenerator.Test.Unit.Calculator
{
    [TestClass]
    public class TaxByThresholdTest
    {
        private TaxByThreshold _subject;
        private readonly Mock<IConfiguration> _configuration = new Mock<IConfiguration>();

        [TestInitialize]
        public void SetUp()
        {
            _subject = new TaxByThreshold(_configuration.Object, new DataTable());
        }

        [TestMethod]
        public void CalculateTax_Tier0Income_ReturnValidTax()
        {
            //Arrange
            var annualSalary = 15000;
            var expression = "AnnualSalary*0";
            var expected = 0;

            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value)
                .Returns(expression);
            _configuration.Setup(a => a.GetSection(Constants.TaxByThresholdTier0))
                .Returns(configurationSection.Object);

            //Act
            var actual = _subject.CalculateTax(annualSalary, Constants.TaxByThresholdTier0);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CalculateTax_Tier1Income_ReturnValidTax()
        {
            //Arrange
            var annualSalary = 20000;
            var expression = "((AnnualSalary-18200)*0.19)/12";
            var expected = 28;

            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value)
                .Returns(expression);
            _configuration.Setup(a => a.GetSection(Constants.TaxByThresholdTier1))
                .Returns(configurationSection.Object);

            //Act
            var actual = _subject.CalculateTax(annualSalary, Constants.TaxByThresholdTier1);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CalculateTax_Tier2Income_ReturnValidTax()
        {
            //Arrange
            var annualSalary = 60050;
            var expression = "(3572+(AnnualSalary-37000)*0.325)/12";
            var expected = 922;

            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value)
                .Returns(expression);
            _configuration.Setup(a => a.GetSection(Constants.TaxByThresholdTier2))
                .Returns(configurationSection.Object);

            //Act
            var actual = _subject.CalculateTax(annualSalary, Constants.TaxByThresholdTier2);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CalculateTax_Tier3Income_ReturnValidTax()
        {
            //Arrange
            var annualSalary = 120000;
            var expression = "(19822+(AnnualSalary-87000)*0.37)/12";
            var expected = 2669;

            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value)
                .Returns(expression);
            _configuration.Setup(a => a.GetSection(Constants.TaxByThresholdTier3))
                .Returns(configurationSection.Object);

            //Act
            var actual = _subject.CalculateTax(annualSalary, Constants.TaxByThresholdTier3);
            //Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateTax_Tier4Income_ReturnValidTax()
        {
            //Arrange
            var annualSalary = 200000;            
            var expression = "(54232+(AnnualSalary-180000)*0.45)/12";
            var expected = 5269;

            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value)
                .Returns(expression);
            _configuration.Setup(a => a.GetSection(Constants.TaxByThresholdTier4))
                .Returns(configurationSection.Object);

            //Act
            var actual = _subject.CalculateTax(annualSalary, Constants.TaxByThresholdTier4);
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
