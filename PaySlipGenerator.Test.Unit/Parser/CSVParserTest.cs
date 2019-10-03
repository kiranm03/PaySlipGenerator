using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PaySlipGenerator.Factory;
using PaySlipGenerator.Model;
using PaySlipGenerator.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaySlipGenerator.Test.Unit.Parser
{
    [TestClass]
    public class CSVParserTest
    {
        private CSVParser _subject;
        private readonly Mock<ILogger<CSVParser>> _logger = new Mock<ILogger<CSVParser>>();
        private readonly Mock<IEmployeeFactory> _employeeFactory = new Mock<IEmployeeFactory>();
        private const string FIRST_NAME = "TestFirstName";
        private const string LAST_NAME = "TestLastName";
        private const double ANNUAL_SALARY = 140000;
        private const float SUPER_RATE = 9.5F;
        private const string PAYMENT_DATE = "01 March – 31 March";

        [TestInitialize]
        public void SetUp()
        {
            _subject = new CSVParser(_logger.Object, _employeeFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_EmptyCSV_ThrowsArgumentException()
        {
            //Arrange
            var fileData = new string[] { };
            //Act
            var actual = _subject.Parse(fileData);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_CSVHeadersOnly_ThrowsArgumentException()
        {
            //Arrange
            var fileData = new string[] { "first name, last name, annual salary, super rate (%), payment start date" };
            //Act
            var actual = _subject.Parse(fileData);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_CSVIncomplete_ThrowsArgumentException()
        {
            //Arrange
            var fileData = new string[]
            { "first name, last name, annual salary, super rate (%), payment start date",
                $"{FIRST_NAME },{LAST_NAME},{ANNUAL_SALARY},{SUPER_RATE}%"
            };
            //Act
            var actual = _subject.Parse(fileData);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_InvalidSalary_ThrowsArgumentException()
        {
            //Arrange
            var fileData = new string[]
            { "first name, last name, annual salary, super rate (%), payment start date",
                $"{FIRST_NAME },{LAST_NAME},600DH050,{SUPER_RATE}%,{PAYMENT_DATE}"
            };
            //Act
            var actual = _subject.Parse(fileData);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_InvalidSuperRate_ThrowsArgumentException()
        {
            //Arrange
            var fileData = new string[]
            { "first name, last name, annual salary, super rate (%), payment start date",
            $"{FIRST_NAME },{LAST_NAME},{ANNUAL_SALARY},9DV%,{PAYMENT_DATE}"                
            };
            //Act
            var actual = _subject.Parse(fileData);
            //Assert
        }

        [TestMethod]
        public void Parse_ValidCSV_ReturnsParsedResult()
        {
            //Arrange
            var fileData = new string[]
            { "first name, last name, annual salary, super rate (%), payment start date",
                $"{FIRST_NAME },{LAST_NAME},{ANNUAL_SALARY},{SUPER_RATE}%,{PAYMENT_DATE}"
            };
            var employee = new Employee("David", "Rudd", 60050, 9);
            _employeeFactory
                .Setup(x => x.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<float>()))
                .Returns(employee);
            //Act
            var actual = _subject.Parse(fileData);
            //Assert
            Assert.AreEqual(employee, actual.First().Employee);
            Assert.AreEqual(3, actual.First().PaymentDate.Month);
        }
    }
}
