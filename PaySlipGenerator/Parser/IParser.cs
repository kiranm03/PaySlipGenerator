using PaySlipGenerator.Model;

namespace PaySlipGenerator.Parser
{
    public interface IParser
    {
        ParseResult[] Parse(string[] fileData);
    }
}
