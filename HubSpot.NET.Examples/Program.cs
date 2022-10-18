using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HubSpot.NET.Core;

namespace HubSpot.NET.Examples
{

    public class Examples
    {
        private static string[] _args;

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configuration =>
                {
                    configuration
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.Development.json", true, true);

                    configuration.Build();
                    configuration.AddEnvironmentVariables();
                })

                .ConfigureServices((context, services) =>
                {
                    services.AddLogging(logger =>
                    {
                        logger.AddConsole();
                        logger.SetMinimumLevel(LogLevel.Debug);
                    });
                });
        // enable args to be presented from CLI for automated test execution 
        static void Main(string[] args)
        {
            _args = args;
            var host = CreateHostBuilder(args);
            var container = host.Build();
            var configuration = container.Services.GetService<IConfiguration>();
            
            

        }

        // private static void RunApiKeyExamples(HubSpotApi hapiApi)
        // {
        //     var test = new Dictionary<string, string>()
        //     {
        //         {"make", "make"},
        //         {"model", "make"},
        //         {"year", "make"}
        //     };
        //
        //
        //     var newEquipment = new EquipmentObject(schemaId)
        //     {
        //         Year = "2006",
        //         Make = "Ford",
        //         Model = "150",
        //     };
        //     var result1 = hapiApi.CustomObjects.Create(newEquipment);
        // }

        // public class EquipmentObject : CustomObjectHubSpotModel
        // {
        //     public EquipmentObject(string objectId) : base(objectId)
        //     {
        //         
        //     }
        //
        //     [DataMember(Name ="name")]
        //     public new string Name => $"{Year} {Make} {Model}";
        //         
        //     [DataMember(Name ="make")]
        //     public string Make { get; set; }
        //     [DataMember(Name ="model")]
        //     public string Model { get; set; }
        //     
        //     // [DataMember(Name ="year")]
        //     public string Year { get; set; }
        //     
        // }


  
    }
}
