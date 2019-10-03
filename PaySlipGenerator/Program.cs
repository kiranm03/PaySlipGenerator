using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaySlipGenerator.Calculator;
using PaySlipGenerator.Factory;
using PaySlipGenerator.Parser;
using PaySlipGenerator.Worker;
using System;
using System.Data;
using System.IO;

namespace PaySlipGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();            

            serviceProvider.GetService<IProcessor>().Process();
        }

        private static IConfigurationRoot ConfigureSettings()
        {
            var builder = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json");
            return builder.Build();
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(ConfigureSettings());

            services.AddSingleton<IProcessor, Processor>();
            services.AddSingleton<IFileReader, FileReader>();
            services.AddSingleton<IPaySlipGenerator, Worker.PaySlipGenerator>();
            services.AddSingleton<IFileWriter, FileWriter>();

            services.AddSingleton<IIncomeTaxCalculator, IncomeTaxCalculator>();
            services.AddSingleton<IIncomeTaxStrategy, IncomeTaxStrategy>();
            services.AddSingleton<Tier0Threshold>();
            services.AddSingleton<Tier1Threshold>();
            services.AddSingleton<Tier2Threshold>();
            services.AddSingleton<Tier3Threshold>();
            services.AddSingleton<Tier4Threshold>();

            services.AddSingleton<DataTable>();

            services.AddSingleton<IParser, CSVParser>();

            services.AddSingleton<IEmployeeFactory, EmployeeFactory>();
            services.AddSingleton<IIncomeTaxStrategyFactory, IncomeTaxStrategyFactory>();
            services.AddSingleton<ITaxByThreshold[]>(provider=> 
            {
                var factory = provider.GetService(typeof(IIncomeTaxStrategyFactory)) as IIncomeTaxStrategyFactory;
                return factory.Create();
            });

            services
                .AddLogging(cfg => cfg.AddConsole())
                .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug);

            return services;
        }
    }
}
