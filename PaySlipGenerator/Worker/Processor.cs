using Microsoft.Extensions.Logging;
using PaySlipGenerator.Parser;
using System;
using System.Linq;

namespace PaySlipGenerator.Worker
{
    public class Processor : IProcessor
    {
        private readonly IFileReader _fileReader;
        private readonly ILogger _logger;
        private readonly IParser _parser;
        private readonly IPaySlipGenerator _paySlipGenerator;
        private readonly IFileWriter _fileWriter;

        public Processor(ILogger<Processor> logger, 
            IFileReader fileReader, IParser parser, 
            IPaySlipGenerator paySlipGenerator, IFileWriter fileWriter)
        {
            _logger = logger;
            _fileReader = fileReader;
            _parser = parser;
            _paySlipGenerator = paySlipGenerator;
            _fileWriter = fileWriter;
        }
        public void Process()
        {
            _logger.LogInformation("App Started");
            try
            {
                Console.WriteLine("Enter the file path");
                var filePath = Console.ReadLine();

                var fileData = _fileReader.Read(filePath);                

                var parseResults = _parser.Parse(fileData);                

                var payslips = _paySlipGenerator.Generate(parseResults);
                                
                _fileWriter.Write(payslips);                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine($"Something went wrong while generating payslip: {ex.Message}");
            }
            _logger.LogInformation("App Finished");
        }
    }
}
