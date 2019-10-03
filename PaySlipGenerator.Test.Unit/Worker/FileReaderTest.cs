using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PaySlipGenerator.Worker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PaySlipGenerator.Test.Unit.Worker
{
    [TestClass]
    public class FileReaderTest
    {
        private FileReader _subject;
        private readonly Mock<ILogger<FileReader>> _logger = new Mock<ILogger<FileReader>>();

        [TestInitialize]
        public void SetUp()
        {
            _subject = new FileReader(_logger.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Read_FilePathIsEmpty_ThrowException()
        {
            //Arrange
            var filePath = string.Empty;
            //Act
            _subject.Read(filePath);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Read_FileDoesNotExist_ThrowException()
        {
            //Arrange
            var filePath = "Z:/test.txt";
            //Act
            _subject.Read(filePath);
            //Assert
        }
    }
}
