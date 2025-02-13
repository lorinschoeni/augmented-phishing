using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PhishAR.ETHTB.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DisplayState
    {
        [EnumMember(Value = "onStart")] OnStart,
        [EnumMember(Value = "onToggle")] OnToggle,
        [EnumMember(Value = "onClick")] OnClick
    }
}
