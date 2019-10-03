using Microsoft.Extensions.Configuration;
using PaySlipGenerator.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PaySlipGenerator.Calculator
{
    public class Tier2Threshold : TaxByThreshold, ITaxByThreshold
    {
        public Tier2Threshold(IConfiguration configuration, DataTable dataTable)
            : base(configuration, dataTable) { }

        public TaxThreshold taxThreshold { get { return TaxThreshold.Tier2; } }

        public double CalculateTax(double annualSalary)
        {
            return CalculateTax(annualSalary, Constants.TaxByThresholdTier2);
        }
    }
}
