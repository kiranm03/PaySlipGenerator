using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaySlipGenerator.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaySlipGenerator.Calculator
{
    public class IncomeTaxCalculator : IIncomeTaxCalculator
    {
        private readonly ILogger _logger;
        private readonly IIncomeTaxStrategy _incomeTaxStrategy;
        private readonly IConfiguration _configuration;
        public IncomeTaxCalculator
            (ILogger<IncomeTaxCalculator> logger, IIncomeTaxStrategy incomeTaxStrategy, IConfiguration configuration)
        {
            _logger = logger;
            _incomeTaxStrategy = incomeTaxStrategy;
            _configuration = configuration;
        }
        public double Calculate(double annualSalary)
        {
            var threshold = IdentifyThreshold(annualSalary);
            return _incomeTaxStrategy.Calculate(annualSalary, threshold);
        }

        private TaxThreshold IdentifyThreshold(double annualSalary)
        {
            switch (annualSalary)
            {
                case double sal when double.IsNegative(sal):
                    _logger.LogCritical("Annual salary is negative");
                    throw new ArgumentException(nameof(annualSalary));
                case double sal when sal < _configuration.GetValue<double>(Constants.IncomeThresholdTier0):
                    return TaxThreshold.Tier0;
                case double sal when sal < _configuration.GetValue<double>(Constants.IncomeThresholdTier1):
                    return TaxThreshold.Tier1;
                case double sal when sal < _configuration.GetValue<double>(Constants.IncomeThresholdTier2):
                    return TaxThreshold.Tier2;
                case double sal when sal < _configuration.GetValue<double>(Constants.IncomeThresholdTier3):
                    return TaxThreshold.Tier3;
                case double sal when sal > _configuration.GetValue<double>(Constants.IncomeThresholdTier4):
                    return TaxThreshold.Tier4;
                default:
                    throw new ArgumentException(nameof(annualSalary));
            }
        }
    }
}
