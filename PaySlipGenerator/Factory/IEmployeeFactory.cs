using PaySlipGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaySlipGenerator.Factory
{
    public interface IEmployeeFactory
    {
        Employee Create(string firstName, string lastName, double annualSalary, float superRate);
    }
}
