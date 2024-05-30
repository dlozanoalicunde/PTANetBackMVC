using System.Text.Json.Serialization;

namespace MBAOptionsManager.Model
{
    public class ExternalMBAOption
    {
        public int? Id { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }

        [JsonPropertyName("mbas")]
        public List<ExternalMBA> MBAs { get; set; }
    }
}
