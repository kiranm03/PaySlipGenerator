using System;
using System.Collections.Generic;
using System.Text;

namespace PaySlipGenerator.Calculator
{
    public interface ITaxByThreshold
    {
        TaxThreshold taxThreshold { get; }

        double CalculateTax(double annualSalary);
    }
}
