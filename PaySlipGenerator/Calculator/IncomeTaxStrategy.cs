using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaySlipGenerator.Calculator
{
    public class IncomeTaxStrategy : IIncomeTaxStrategy
    {
        private readonly ITaxByThreshold[] _taxByThresholds;
        public IncomeTaxStrategy(ITaxByThreshold[] taxByThresholds)
        {
            _taxByThresholds = taxByThresholds;
        }
        public double Calculate(double annualSalary, TaxThreshold taxThreshold)
        {
            return _taxByThresholds
                    .First(t => t.taxThreshold.Equals(taxThreshold))
                    .CalculateTax(annualSalary);
        }
    }
}
