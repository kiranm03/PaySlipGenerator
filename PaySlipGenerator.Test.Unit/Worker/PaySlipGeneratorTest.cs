using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using W = PaySlipGenerator.Worker;
using System;
using System.Collections.Generic;
using System.Text;
using PaySlipGenerator.Calculator;
using PaySlipGenerator.Model;
using System.Linq;

namespace PaySlipGenerator.Test.Unit.Worker
{
    [TestClass]
    public class PaySlipGeneratorTest
    {
        private W.PaySlipGenerator _subject;
        private readonly Mock<ILogger<W.PaySlipGenerator>> _logger = new Mock<ILogger<W.PaySlipGenerator>>();
        private readonly Mock<IIncomeTaxCalculator> _incomeTaxCalculator = new Mock<IIncomeTaxCalculator>();

        [TestInitialize]
        public void SetUp()
        {
            _subject = new W.PaySlipGenerator(_logger.Object, _incomeTaxCalculator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Generate_WithoutEmployeeData_ThrowsException()
        {
            //Arrange
            var parseResults = new ParseResult[] {
            new ParseResult()
            {
                Employee= null,
                PaymentDate= DateTime.Now
            }
            };
            //Act
            _subject.Generate(parseResults);
            //Assert
        }

        [TestMethod]
        public void Generate_ValidParseResult_ReturnsPayslip()
        {
            //Arrange
            var parseResults = new ParseResult[] {
                new ParseResult()
                {
                Employee = new Employee("David","Rudd",60050,9),
                PaymentDate = DateTime.Now
                }
            };
            _incomeTaxCalculator
                .Setup(x => x.Calculate(It.IsAny<double>()))
                .Returns(922);
            //Act
            var actual = _subject.Generate(parseResults);
            //Assert
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual("David Rudd", actual.First().EmployeeName);
            Assert.AreEqual(parseResults.First().PaymentDate, actual.First().PayPeriod);
            Assert.AreEqual(5004, actual.First().GrossIncome);
            Assert.AreEqual(4082, actual.First().NetIncome);
            Assert.AreEqual(922, actual.First().IncomeTax);
            Assert.AreEqual(450, actual.First().Super);
        }
    }
}
