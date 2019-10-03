using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using PaySlipGenerator.Extension;
using PaySlipGenerator.Factory;
using PaySlipGenerator.Model;

namespace PaySlipGenerator.Parser
{
    public class CSVParser : IParser
    {
        private readonly ILogger _logger;
        private readonly IEmployeeFactory _employeeFactory;

        public CSVParser(ILogger<CSVParser> logger, IEmployeeFactory employeeFactory)
        {
            _logger = logger;
            _employeeFactory = employeeFactory;
        }
        public ParseResult[] Parse(string[] fileData)
        {
            ValidateFileData(fileData);

            //skip header row
            return fileData
                    .Skip(1)
                    .Select(d => GetParseResult(d))
                    .ToArray();
        }

        private ParseResult GetParseResult(string data)
        {
            var dataArray = data.Split(',');
            GetEmployeeData(dataArray, out string firstName, out string lastName, out double annualSalary, out float superRate);

            return new ParseResult()
            {
                Employee = _employeeFactory.Create(firstName, lastName, annualSalary, superRate),
                PaymentDate = dataArray
                                .Last()
                                .ToDate()
            };
        }

        private void GetEmployeeData(string[] dataArray, out string firstName, out string lastName, out double annualSalary, out float superRate)
        {
            ValidateDataRow(dataArray, out annualSalary, out superRate);

            firstName = dataArray[0];
            lastName = dataArray[1];
            //annualSalary = Convert.ToDouble(dataArray[2]);
            //superRate = float.Parse(dataArray[3].Replace("%", ""));
        }

        private void ValidateDataRow(string[] dataArray, out double annualSalary, out float superRate)
        {
            if (dataArray.Length < 5)
            {
                _logger.LogCritical("CSV data is InValid");
                throw new ArgumentException(nameof(dataArray));
            }

            if (!double.TryParse(dataArray[2], out annualSalary))
            {
                _logger.LogCritical($"Annual salary is InValid: {annualSalary}");
                throw new ArgumentException(nameof(annualSalary));
            }

            if (!float.TryParse(dataArray[3].Replace("%", ""), out superRate))
            {
                _logger.LogCritical($"Super rate is InValid: {superRate}");
                throw new ArgumentException(nameof(superRate));
            }
        }

        private void ValidateFileData(string[] fileData)
        {
            //check if csv has rows other than headers
            if (fileData.Length < 2)
            {
                _logger.LogCritical("File data is InValid");
                throw new ArgumentException(nameof(fileData));
            }
        }
    }
}
