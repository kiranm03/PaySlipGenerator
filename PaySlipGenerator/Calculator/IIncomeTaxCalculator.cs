using System;
using System.Collections.Generic;
using System.Text;

namespace PaySlipGenerator.Calculator
{
    public interface IIncomeTaxCalculator
    {
        double Calculate(double annualSalary);
    }
}
