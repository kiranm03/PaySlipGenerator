using System;
using System.Collections.Generic;
using System.Text;
using PaySlipGenerator.Calculator;

namespace PaySlipGenerator.Factory
{
    public class IncomeTaxStrategyFactory : IIncomeTaxStrategyFactory
    {
        private readonly Tier0Threshold _tier0Threshold;
        private readonly Tier1Threshold _tier1Threshold;
        private readonly Tier2Threshold _tier2Threshold;
        private readonly Tier3Threshold _tier3Threshold;
        private readonly Tier4Threshold _tier4Threshold;
        public IncomeTaxStrategyFactory(Tier0Threshold tier0Threshold, Tier1Threshold tier1Threshold, Tier2Threshold tier2Threshold,
            Tier3Threshold tier3Threshold, Tier4Threshold tier4Threshold)
        {
            _tier0Threshold = tier0Threshold;
            _tier1Threshold = tier1Threshold;
            _tier2Threshold = tier2Threshold;
            _tier3Threshold = tier3Threshold;
            _tier4Threshold = tier4Threshold;
        }
        public ITaxByThreshold[] Create()
        {
            return new ITaxByThreshold[] { _tier0Threshold, _tier1Threshold, _tier2Threshold, _tier3Threshold, _tier4Threshold };
        }
    }
}
