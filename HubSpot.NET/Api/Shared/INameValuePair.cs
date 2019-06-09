using System;
using System.Collections.Generic;
using System.Text;

namespace HubSpot.NET.Api.Shared
{
    public interface INameValuePair
    {
        string Name { get; set; }
        string Value { get; set; }
    }
}
