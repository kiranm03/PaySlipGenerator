using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PaySlipGenerator.Model;
using PaySlipGenerator.Worker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PaySlipGenerator.Test.Unit.Worker
{
    [TestClass]
    public class FileWriterTest
    {
        private FileWriter _subject;
        private readonly Mock<ILogger<FileWriter>> _logger = new Mock<ILogger<FileWriter>>();

        [TestInitialize]
        public void SetUp()
        {
            _subject = new FileWriter(_logger.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Write_NullInput_ThrowsException()
        {
            //Arrange
            PaySlip[] payslips = null;
            //Act
            _subject.Write(payslips);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Write_EmptyInput_ThrowsException()
        {
            //Arrange
            PaySlip[] payslips = new PaySlip[] { };
            //Act
            _subject.Write(payslips);
            //Assert
        }

        [TestMethod]
        public void Write_ValidPayslips_SavesToFile()
        {
            //Arrange
            PaySlip[] payslips = new PaySlip[]
            {
                new PaySlip("testName",DateTime.Now,1200,300,900,56)
            };
            //Act
            _subject.Write(payslips);
            //Assert
            Assert.IsTrue(File.Exists("output.csv"));
        }
    }
}
