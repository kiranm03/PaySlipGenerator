using System;
using System.Collections.Generic;
using System.Text;

namespace PaySlipGenerator.Calculator
{
    public interface IIncomeTaxStrategy
    {
        double Calculate(double annualSalary, TaxThreshold taxThreshold);
    }
}
