using System;
using System.Collections.Generic;
using System.Text;

namespace PaySlipGenerator.Model
{
    public class ParseResult
    {
        public Employee Employee { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
