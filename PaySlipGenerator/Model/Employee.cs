using System;

namespace PaySlipGenerator.Model
{
    public class Employee
    {
        private readonly string _firstName;
        private readonly string _lastName;

        public Employee(string firstName, string lastName, double annualSalary, float superRate)
        {
            _firstName = firstName;
            _lastName = lastName;
            AnnualSalary = annualSalary;
            SuperRate = superRate;
        }

        public string Name
        {
            get
            {
                return $"{_firstName} {_lastName}";
            }
        }
        public double AnnualSalary { get; }
        public float SuperRate { get; }
    }
}
