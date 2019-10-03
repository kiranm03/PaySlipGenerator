using Microsoft.Extensions.Configuration;
using PaySlipGenerator.Util;
using System.Data;

namespace PaySlipGenerator.Calculator
{
    public class Tier3Threshold : TaxByThreshold, ITaxByThreshold
    {
        public Tier3Threshold(IConfiguration configuration, DataTable dataTable)
            : base(configuration, dataTable) { }

        public TaxThreshold taxThreshold { get { return TaxThreshold.Tier3; } }

        public double CalculateTax(double annualSalary)
        {
            return CalculateTax(annualSalary, Constants.TaxByThresholdTier3);
        }
    }
}
