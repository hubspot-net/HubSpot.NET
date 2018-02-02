[![NuGet](https://img.shields.io/nuget/v/SquaredUp.HubSpot.NET.svg)](https://www.nuget.org/packages/NUnit/)

# HubSpot.NET
C# .NET Wrapper around the common HubSpot APIs:

* Contact
* Company
* Deal
* Engagement
* Owners
* COS Files API (adds the ability to upload files to use as attachments to engagements)

## Getting Started
To get started, install the Nuget package and create a instance of HubSpotApi passing your API Key as the parameter. 

```
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
**For more examples see the HubSpot.NET.Examples project.**

### Using your own models
As HubSpot lets you create and add custom properties to your contacts, companies and deals it's likely you'll want to implement your own models. This is straightforward, simply extend the models shipped with this library, e.g. `ContactHubSpotModel` and add your own properties. Use the `DataMember` attributes to indicate the internal name. For example

```
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

