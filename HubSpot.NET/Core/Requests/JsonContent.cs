using System.Net.Http;
using System.Text;

namespace HubSpot.NET.Core.Requests
{
    public class JsonContent : StringContent
    {
        public JsonContent(string json) : this(json, Encoding.UTF8)
        {
        }

        public JsonContent(string json, Encoding encoding) : base(json, encoding, "application/json")
        {
        }
    }
}