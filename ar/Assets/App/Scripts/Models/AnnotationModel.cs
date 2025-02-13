using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace PhishAR.ETHTB.Models
{
    public class AnnotationModel
    {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("label")] public string Label { get; set; }
        [JsonProperty("offset")] private List<float> Offset { get; set; }
        [JsonProperty("dimensions")] private List<float> Dimensions { get; set; }
        [JsonProperty("borderColor")] public string BorderColorHex { get; set; }
        [JsonProperty("textColor")] public string TextColorHex { get; set; }
        [JsonProperty("textSize")] public float TextSize { get; set; }
        [JsonProperty("icon")] public string Icon { get; set; }

        public float Width => Dimensions[0] / 100;

        public float Height => Dimensions[1] / 100;

        public float OffsetX => Offset[0] / 100;

        // Fix for bad offset from web.
        public float OffsetY => Offset[1] / 100 + -.09f * (Offset[1] / 100) + 0.12f;

        public Color BorderColor => FromString(BorderColorHex);
        public Color TextColor => FromString(TextColorHex);

        private Color FromString(string from)
        {
            if (!ColorUtility.TryParseHtmlString(from, out var color))
                Debug.Log($"{from} is not a valid text color");
            return color;
        }
    }
}
