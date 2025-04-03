using Newtonsoft.Json;

namespace KnuddelsAdmin.Protocol;

class DeviceInfo {
    [JsonProperty("name")]
    public String? Name { get; set; }

    [JsonProperty("type")]
    public String? Type { get; set; }

    [JsonProperty("size")]
    public Dimension? Size { get; set; }
}
