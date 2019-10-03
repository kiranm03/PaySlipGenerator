using Microsoft.Extensions.Configuration;
using PaySlipGenerator.Util;
using System.Data;

namespace PaySlipGenerator.Calculator
{
    public class Tier4Threshold : TaxByThreshold, ITaxByThreshold
    {
        public Tier4Threshold(IConfiguration configuration, DataTable dataTable)
            : base(configuration, dataTable) { }

        public TaxThreshold taxThreshold { get { return TaxThreshold.Tier4; } }

        public double CalculateTax(double annualSalary)
        {
            return CalculateTax(annualSalary, Constants.TaxByThresholdTier4);
        }
    }
}
