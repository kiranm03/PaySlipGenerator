using System;
using System.Collections.Generic;
using System.Text;

namespace PaySlipGenerator.Model
{
    public class PaySlip
    {
        public PaySlip(string employeeName, DateTime payPeriod, double grossIncome, double incomeTax, double netIncome, double super)
        {
            EmployeeName = employeeName;
            PayPeriod = payPeriod;
            GrossIncome = grossIncome;
            IncomeTax = incomeTax;
            NetIncome = netIncome;
            Super = super;
        }

        public string EmployeeName { get; }
        public DateTime PayPeriod { get; }
        public double GrossIncome { get; }
        public double IncomeTax { get; }
        public double NetIncome { get; }
        public double Super { get; }
    }
}
