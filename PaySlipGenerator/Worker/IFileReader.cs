namespace PaySlipGenerator.Worker
{
    public interface IFileReader
    {
        string[] Read(string filePath);
    }
}