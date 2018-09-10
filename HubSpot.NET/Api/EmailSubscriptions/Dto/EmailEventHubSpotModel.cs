using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Api.EmailSubscriptions.Dto
{
    public class EmailEventHubSpotModel
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "created")]
        public long Created { get; set; }
    }
}
