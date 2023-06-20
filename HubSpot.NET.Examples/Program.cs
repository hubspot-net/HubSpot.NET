using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using HubSpot.NET.Api.CustomObject;
using HubSpot.NET.Api.Files.Dto;
using HubSpot.NET.Api.Note.Dto;
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
        static async Task Main(string[] args)
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
            var newEquipmentId = api.CustomObjects.CreateWithDefaultAssociationToObject(newEquipment, "0-3", "9909067546");
            
            
            var result3 = api.CustomObjects.GetAssociationsToCustomObject
                <CustomObjectAssociationModel>("2-4390924", "3254092177",
                "0-1", CancellationToken.None);
            
            
            
            // 0-3 -> deal object type
            // 9346274448 -> deal id
            // 0-1 -> contact object type
            // 68751 -> contact id
            // USER_DEFINED -> associationCategory
            // 55 -> association label
            // api.Associations.AssociationToObjectByLabel("0-3", "9346274448", "0-1", "68751", "USER_DEFINED", 55);
            
            
            var updatedEquipment = new UpdateCustomObjectHubSpotModel
            {
                Id = newEquipmentId,
                SchemaId = id,
                Properties = new Dictionary<string, object>()
                {
                    {"year1", 2024},
                    {"make", "Ford"},
                    {"model", "550" + DateTime.Now.Hour + DateTime.Now.Minute},
                    {"name", $"2024 Ford 550"}
                }
            };
            
            var updatedResultId = api.CustomObjects.UpdateObject(updatedEquipment);
            Console.Write(updatedResultId);


            await UploadNoteWithFile(api);
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


        static async Task UploadNoteWithFile(HubSpotApi api)
        {
            var httpClient = new HttpClient();
            byte[] fileBuffer = null;
            try
            {
                fileBuffer = await httpClient.GetByteArrayAsync(
                    "https://images.immediate.co.uk/production/volatile/sites/4/2021/08/mountains-7ddde89.jpg");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           

            var fileModel = new FileHubSpotRequestModel()
            {
                File = fileBuffer,
                Name = "mountains.jpg",
                FolderPath = "/docs",
                Options = new FileHubSpotRequestOptionsModel()
                {
                    Access = "PRIVATE",
                    TTL = "P3M",
                    Overwrite = false,
                    DuplicateValidationStrategy = "NONE",
                    DuplicateValidationScope = "EXACT_FOLDER"
                }
            };

            var fileResponse = api.File.UploadFile(fileModel);
            
            Console.Write(fileResponse);

            
            var note = new NoteHubSpotRequestModel()
            {
                Properties = new NoteHubSpotRequestPropertiesModel()
                {
                    HsTimestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    HsNoteBody = "Hello there from nuget again, with a new file 3",
                    HubspotOwnerId = "",
                    HsAttachmentIds = fileResponse?.Objects?.First().Id.ToString(),
                },
                Associations = new List<NoteHubSpotRequestAssociationsModel>
                {
                    new NoteHubSpotRequestAssociationsModel
                    {
                        To = new NoteHubSpotRequestAssociationToModel {Id = "12792130062"},
                        Types = new List<NoteHubspotRequestAssociationTypeModel>
                        {
                            new NoteHubspotRequestAssociationTypeModel()
                                {AssociationCategory = "HUBSPOT_DEFINED", AssociationTypeId = "214"}
                        }
                    }
                }
            };
            
            var noteResponse = api.Note.Create(note);
            
            Console.Write(noteResponse);
        }


  
    }
}
