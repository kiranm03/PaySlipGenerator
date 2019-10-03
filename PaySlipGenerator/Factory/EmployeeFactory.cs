using System;
using System.Collections.Generic;
using System.Text;
using PaySlipGenerator.Model;

namespace PaySlipGenerator.Factory
{
    public class EmployeeFactory : IEmployeeFactory
    {
        public Employee Create(string firstName, string lastName, double annualSalary, float superRate)
        {
            return new Employee(firstName, lastName, annualSalary, superRate);
        }
    }
}
