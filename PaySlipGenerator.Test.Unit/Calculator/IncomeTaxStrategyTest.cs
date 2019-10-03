using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PaySlipGenerator.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaySlipGenerator.Test.Unit.Calculator
{
    [TestClass]
    public class IncomeTaxStrategyTest
    {
        private IncomeTaxStrategy _subject;
        private readonly Mock<ITaxByThreshold> _taxByThreshold = new Mock<ITaxByThreshold>();

        [TestInitialize]
        public void SetUp()
        {
            _subject = new IncomeTaxStrategy(new ITaxByThreshold[] { _taxByThreshold.Object });
        }

        [TestMethod]
        public void Calculate_ValidInput_InvokeCalculateTaxMethod()
        {
            //Arrange
            var annualSalary = 15000;

            //Act
            _subject.Calculate(annualSalary, TaxThreshold.Tier0);
            //Assert
            _taxByThreshold.Verify(x => x.CalculateTax(annualSalary), Times.Once);
        }
    }
}
