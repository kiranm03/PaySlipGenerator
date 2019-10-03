using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using PaySlipGenerator.Calculator;
using PaySlipGenerator.Model;
using System.Linq;
using PaySlipGenerator.Worker;
using PaySlipGenerator.Parser;

namespace PaySlipGenerator.Test.Unit.Worker
{
    [TestClass]
    public class ProcessorTest
    {
        private Processor _subject;
        private readonly Mock<ILogger<Processor>> _logger = new Mock<ILogger<Processor>>();
        private readonly Mock<IIncomeTaxCalculator> _incomeTaxCalculator = new Mock<IIncomeTaxCalculator>();
        private readonly Mock<IFileReader> _fileReader = new Mock<IFileReader>();
        private readonly Mock<IParser> _parser = new Mock<IParser>();
        private readonly Mock<IPaySlipGenerator> _paySlipGenerator = new Mock<IPaySlipGenerator>();
        private readonly Mock<IFileWriter> _fileWriter = new Mock<IFileWriter>();

        [TestInitialize]
        public void SetUp()
        {
            _subject = new Processor(_logger.Object, _fileReader.Object, _parser.Object, _paySlipGenerator.Object, _fileWriter.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Process_ParserFails_ThrowsException()
        {
            //Arrange
            _parser.Setup(x => x.Parse(It.IsAny<string[]>()))
                .Throws<ArgumentException>();
            //Act
            _subject.Process();
            //Assert
        }

        [TestMethod]
        public void Process_ValidData_ProcessWithoutErrors()
        {
            //Arrange
            _fileReader
                .Setup(x => x.Read(It.IsAny<string>()))
                .Returns(It.IsAny<string[]>());
            _parser
                .Setup(x => x.Parse(It.IsAny<string[]>()))
                .Returns(It.IsAny<ParseResult[]>());
            _paySlipGenerator
                .Setup(x => x.Generate(It.IsAny<ParseResult[]>()))
                .Returns(It.IsAny<PaySlip[]>());

            //Act
            _subject.Process();
            //Assert
            _fileWriter.Verify(x => x.Write(It.IsAny<PaySlip[]>()), Times.Once);
        }
    }
}
