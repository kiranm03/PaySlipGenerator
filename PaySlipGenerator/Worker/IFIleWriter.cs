using PaySlipGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaySlipGenerator.Worker
{
    public interface IFileWriter
    {
        void Write(PaySlip[] payslips);
    }
}
