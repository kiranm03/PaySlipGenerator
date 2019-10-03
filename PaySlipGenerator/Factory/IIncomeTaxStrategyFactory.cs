using PaySlipGenerator.Calculator;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaySlipGenerator.Factory
{
    public interface IIncomeTaxStrategyFactory
    {
        ITaxByThreshold[] Create();
    }
}
