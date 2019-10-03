using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace PaySlipGenerator.Worker
{
    public class FileReader : IFileReader
    {
        private readonly ILogger _logger;

        public FileReader(ILogger<FileReader> logger)
        {
            _logger = logger;
        }
        public string[] Read(string filePath)
        {   
            ValidateFilePath(filePath);

            try
            {
                var fileData = File.ReadAllLines(@filePath);
                return fileData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception($"Something went wrong while reading the file: {ex.Message}");
            }
        }

        private void ValidateFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                _logger.LogCritical("File path is empty");
                throw new ArgumentNullException("File path should not be empty", filePath);
            }

            if (!File.Exists(filePath))
            {
                _logger.LogCritical($"File is not found at {filePath}");
                throw new FileNotFoundException("Unable to locate the file", filePath);
            }
        }
    }
}
