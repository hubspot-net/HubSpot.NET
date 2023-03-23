using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using HubSpot.NET.Api.CustomObject;
using HubSpot.NET.Api.Schemas;
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

            var api = new HubSpotApi(configuration["HubSpot:PrivateAppKey"]);

            var customSchemas = api.Schema.List<SchemaHubSpotModel>();
            var id = "2-" + customSchemas.Results.First(x => x.Name == "Machine2").Id;
            
            // get the equipment id off seller id...
            
            var newEquipment = new CreateCustomObjectHubSpotModel
            {
                SchemaId = id,
                Properties = new Dictionary<string, object>()
                {
                    
                    {"year1", 2014},
                    {"make", "Ford"},
                    {"model", "150" + DateTime.Now.Hour + DateTime.Now.Minute},
                    {"name", $"2015 Ford 150"}
                },
                Associations = new List<CreateCustomObjectHubSpotModel.Association>()
                {
                    new()
                    {
                        To = new CreateCustomObjectHubSpotModel.To()
                        {
                            Id = "15273381013"  // company to associate to 
                        },
                        Types = new List<CreateCustomObjectHubSpotModel.TypeElement>()
                        {
                            new()
                            {
                                AssociationCategory = "USER_DEFINED", 
                                AssociationTypeId = 53 // id of the label that we want to assign it.
                            }
                        }
                    },
                    new()
                    {
                        To = new CreateCustomObjectHubSpotModel.To()
                        {
                            Id = "68751"  // contact to associate to 
                        },
                        Types = new List<CreateCustomObjectHubSpotModel.TypeElement>()
                        {
                            new()
                            {
                                AssociationCategory = "USER_DEFINED", 
                                AssociationTypeId = 55 // id of the label that we want to assign it.
                            }
                        }
                    }
                }
            };
            // 0-3 => object type id that corresponds to the deal
            // 9909067546 => deal id
            var result1 = api.CustomObjects.CreateWithDefaultAssociationToObject(newEquipment, "0-3", "9909067546");
            

            var result3 = api.CustomObjects.GetAssociationsToCustomObject
                <CustomObjectAssociationModel>("2-4390924", "3254092177",
                "0-1", CancellationToken.None);



        }



        public class EquipmentObject : CreateCustomObjectHubSpotModel
        {


            [DataMember(Name ="name")]
            public new string Name => $"{Year} {Make} {Model}";
                
            [DataMember(Name ="make")]
            public string Make { get; set; }
            [DataMember(Name ="model")]
            public string Model { get; set; }
            
            // [DataMember(Name ="year")]
            public string Year { get; set; }
            
        }


  
    }
}
