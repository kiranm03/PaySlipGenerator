using Microsoft.Extensions.Configuration;
using PaySlipGenerator.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PaySlipGenerator.Calculator
{
    public class TaxByThreshold
    {
        private readonly IConfiguration _configuration;
        private readonly DataTable _dataTable;
        public TaxByThreshold(IConfiguration configuration, DataTable dataTable)
        {
            _configuration = configuration;
            _dataTable = dataTable;
        }
        public double CalculateTax(double annualSalary, string TaxByThresholdTier)
        {
            var expression = _configuration.GetValue<string>(TaxByThresholdTier);
            expression = expression
                .Replace(Constants.AnnualSalaryTextInSettings, $"{annualSalary}");

            return Math.Round(Convert.ToDouble(_dataTable.Compute(expression, null)));
        }
    }
}
