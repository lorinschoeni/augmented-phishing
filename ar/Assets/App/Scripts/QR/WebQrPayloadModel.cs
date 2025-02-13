using Newtonsoft.Json;

namespace PhishAR.ETHTB.QR
{
    public class WebQrPayloadModel
    {
        [JsonProperty("api")] public string IpAddress { get; set; }
        [JsonProperty("width")] public float Width { get; set; }
        [JsonProperty("height")] public float Height { get; set; }

        public float QrRelativeWidth => Width / 100;
        public float QrRelativeHeight => Height / 100;
    }
}
