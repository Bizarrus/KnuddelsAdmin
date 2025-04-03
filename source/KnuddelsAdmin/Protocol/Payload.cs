using Newtonsoft.Json;

namespace KnuddelsAdmin.Protocol;

class Payload {
    [JsonProperty("settings")]
    public object? Settings { get; set; }

    [JsonProperty("coordinates")]
    public Path? Coordinates { get; set; }

    [JsonProperty("state")]
    public int? State { get; set; }

    [JsonProperty("isInMultiAction")]
    public bool? Multiaction { get; set; }
}
