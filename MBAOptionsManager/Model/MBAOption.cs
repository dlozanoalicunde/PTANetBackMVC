using System.Text.Json.Serialization;

namespace MBAOptionsManager.Model
{
    public class MBAOption
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        [JsonPropertyName("mbas")]
        public virtual List<MBA> MBAs { get; set; }
    }
}