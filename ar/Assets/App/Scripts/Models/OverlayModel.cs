using System.Collections.Generic;
using Newtonsoft.Json;

namespace PhishAR.ETHTB.Models
{
    public class OverlayModel
    {
        [JsonProperty("annotations")] public List<AnnotationModel> Annotations { get; set; }
        [JsonProperty("target")] public Target Target { get; set; }
    }

    public class Target
    {
        [JsonProperty("annotate")] public DisplayState DisplayState { get; set; }
    }
}
