using Microsoft.Extensions.Configuration;
using PaySlipGenerator.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PaySlipGenerator.Calculator
{
    public class Tier1Threshold : TaxByThreshold, ITaxByThreshold
    {        
        public Tier1Threshold(IConfiguration configuration, DataTable dataTable)
            : base(configuration, dataTable) { }
        public TaxThreshold taxThreshold
        {
            get
            {
                return TaxThreshold.Tier1;
            }
        }

        public double CalculateTax(double annualSalary)
        {           
            return CalculateTax(annualSalary, Constants.TaxByThresholdTier1);
        }
    }
}
