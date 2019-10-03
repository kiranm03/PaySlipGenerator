using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using PaySlipGenerator.Extension;
using PaySlipGenerator.Model;
using PaySlipGenerator.Util;

namespace PaySlipGenerator.Worker
{
    public class FileWriter : IFileWriter
    {
        private readonly ILogger _logger;

        public FileWriter(ILogger<FileWriter> logger)
        {
            _logger = logger;
        }
        public void Write(PaySlip[] payslips)
        {
            ValidatePayslips(payslips);

            try
            {
                var csvPayslipText = payslips
                    .Select(p => AppendToText(p))
                    .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");

                //Add csv header
                csvPayslipText = $"{Constants.CSVOutputHeader}{Environment.NewLine}{csvPayslipText}";

                File.WriteAllText("output.csv", csvPayslipText);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception($"Something went wrong while writing into csv file: {ex.Message}");
            }
        }

        private string AppendToText(PaySlip p)
        {
            return $"{p.EmployeeName},{p.PayPeriod.ToPayslipDateText()},{p.GrossIncome},{p.IncomeTax},{p.NetIncome},{p.Super}";
        }        

        private void ValidatePayslips(PaySlip[] payslips)
        {
            if(payslips is null)
            {
                _logger.LogCritical("Payslips is null");
                throw new ArgumentNullException(nameof(payslips));
            }

            if (payslips.Length < 1)
            {
                _logger.LogCritical("Payslips is empty");
                throw new ArgumentException(nameof(payslips));
            }
        }
    }
}
