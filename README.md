[![NuGet](https://img.shields.io/nuget/v/SquaredUp.HubSpot.NET.svg)](https://www.nuget.org/packages/SquaredUp.HubSpot.NET/) [![Build Status](https://dev.azure.com/hubspot-net/HubSpot.NET/_apis/build/status/hubspot-net.HubSpot.NET?branchName=master)](https://dev.azure.com/hubspot-net/HubSpot.NET/_build/latest?definitionId=1&branchName=master)

# <span>HubSpot.NET</span>
C# .NET Wrapper around the common HubSpot APIs:

* Contact
* Company
* Deal
* Engagement
* Owners
* COS Files API (adds the ability to upload files to use as attachments to engagements)
* Email Subscriptions (currently GET & PUT)
* Timeline API
  * Timeline EventTypes
  * Timeline Events

## Authorization

<span>HubSpot.NET</span> supports authorization by API key or through HubSpot's OAuth workflow. [As is noted in HubSpot's API documentation](), it is recommended to use the OAuth form if your integration is going to be used commercially due to an increased level of security. However, we have made it optional to target OAuth or API key authentication so you can develop against the authentication provider that best suits your needs.

## Getting Started
To get started, install the [Nuget package](https://www.nuget.org/packages/SquaredUp.HubSpot.NET/) and create a instance of `HubSpotApi` passing your API Key as the only parameter; or if using OAuth pass in the Client ID, Client Secret, and App ID.

### API Key
```csharp
  var api = new HubSpotApi("MY API KEY");
  
  // Create a contact
  var contact = api.Contact.Create(new ContactHubSpotModel()
  {
      Email = "john@squaredup.com",
      FirstName = "John",
      LastName = "Smith",
      Phone = "00000 000000",
      Company = "Squared Up Ltd."
  });
  
```

### OAuth
```csharp
  var api = new HubSpotApi("clientID", "clientSecret", "HubSpotAppID");
  
  // Create a contact
  var contact = api.Contact.Create(new ContactHubSpotModel()
  {
      Email = "john@squaredup.com",
      FirstName = "John",
      LastName = "Smith",
      Phone = "00000 000000",
      Company = "Squared Up Ltd."
  });
  
```
**For more examples see the HubSpot.NET.Examples project.**

### Using your own models
As HubSpot lets you create and add custom properties to your contacts, companies and deals it's likely you'll want to implement your own models. This is straightforward, simply extend the models shipped with this library, e.g. `ContactHubSpotModel` and add your own properties. Use the `DataMember` attributes to indicate the internal name. For example

```csharp
  public class Contact : ContactHubSpotModel
  {
      [DataMember(Name = "activities")]
      public string Activities { get; set; }

      [DataMember(Name = "type")]
      public string Type { get; set; }
  }
```
### Using checkbox/radio properties
These properties should be of type `string` and set as a semicolon delimitered list of values, e.g. "value1;value2". This is required by HubSpot, see [here](https://developers.hubspot.com/docs/faq/how-do-i-set-multiple-values-for-checkbox-properties) for more details.

## Contributing
Please read [CONTRIBUTING.md](https://github.com/squaredup/HubSpot.NET/blob/master/CONTRIBUTING.md) for more information on how to contribute. PRs welcome!

## Authors
* Dave Clarke

## License
This project is licensed under the MIT License - see the [LICENSE](https://github.com/squaredup/HubSpot.NET/blob/master/LICENSE) file for details

## Acknowledgements
* Initial version based on dotnetcore-hubspot-client by skarpdev, expanded to additional APIs and heavily refactored to use RestSharp etc.

