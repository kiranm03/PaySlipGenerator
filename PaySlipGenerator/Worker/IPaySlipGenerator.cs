using PaySlipGenerator.Model;

namespace PaySlipGenerator.Worker
{
    public interface IPaySlipGenerator
    {
        PaySlip[] Generate(ParseResult[] parseResults);
    }
}
