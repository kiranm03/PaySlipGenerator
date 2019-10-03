using Microsoft.Extensions.Configuration;
using PaySlipGenerator.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PaySlipGenerator.Calculator
{
    public class Tier0Threshold : TaxByThreshold, ITaxByThreshold
    {
        public Tier0Threshold(IConfiguration configuration, DataTable dataTable)
            : base(configuration, dataTable) { }
        
        public TaxThreshold taxThreshold { get { return TaxThreshold.Tier0; } }

        public double CalculateTax(double annualSalary)
        {
            return CalculateTax(annualSalary, Constants.TaxByThresholdTier0);
        }
    }
}
