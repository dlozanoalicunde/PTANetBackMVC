using System.Text.Json.Serialization;

namespace MBAOptionsManager.Model
{
    public class ExternalMBAOption
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }

        [JsonPropertyName("mbas")]
        public List<ExternalMBA> MBAs { get; set; }
    }
}
