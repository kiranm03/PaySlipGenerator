using Microsoft.Extensions.Logging;
using PaySlipGenerator.Calculator;
using PaySlipGenerator.Model;
using System;
using System.Linq;

namespace PaySlipGenerator.Worker
{
    public class PaySlipGenerator : IPaySlipGenerator
    {
        private readonly ILogger _logger;
        private readonly IIncomeTaxCalculator _incomeTaxCalculator;

        public PaySlipGenerator(ILogger<PaySlipGenerator> logger, IIncomeTaxCalculator incomeTaxCalculator)
        {
            _logger = logger;
            _incomeTaxCalculator = incomeTaxCalculator;
        }

        public PaySlip[] Generate(ParseResult[] parseResults)
        {
            return parseResults
                .Select(r => GeneratePaySlipForEmployee(r))
                .ToArray();
        }

        private PaySlip GeneratePaySlipForEmployee(ParseResult parseResult)
        {
            ValidateParseResult(parseResult);

            var employeeName = parseResult.Employee.Name;
            var payPeriod = parseResult.PaymentDate;
            var grossIncome = CalculateMonthlyGrossIncome(parseResult.Employee.AnnualSalary);
            var incomeTax = _incomeTaxCalculator.Calculate(parseResult.Employee.AnnualSalary);
            var netIncome = CalculateMonthlyNetIncome(grossIncome, incomeTax);
            var super = CalculateMonthlySuper(grossIncome, parseResult.Employee.SuperRate);

            return new PaySlip(employeeName, payPeriod, grossIncome, incomeTax, netIncome, super);
        }

        private double CalculateMonthlyNetIncome(double grossIncome, double incomeTax)
        {
            return grossIncome - incomeTax;
        }

        private double CalculateMonthlySuper(double grossIncome, float superRate)
        {
            return Math.Round(grossIncome * superRate / 100);
        }

        private double CalculateMonthlyGrossIncome(double annualSal)
        {
            return Math.Round(annualSal / 12);
        }

        private void ValidateParseResult(ParseResult parseResult)
        {
            if(parseResult.Employee is null)
            {
                _logger.LogCritical("Employee Data is missing");
                throw new ArgumentNullException(nameof(parseResult.Employee));
            }
        }
    }
}
