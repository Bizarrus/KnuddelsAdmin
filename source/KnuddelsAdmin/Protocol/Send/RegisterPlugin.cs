using Newtonsoft.Json;

namespace KnuddelsAdmin.Protocol.Send;

class RegisterPlugin {
    [JsonProperty("event")]
    public string? EventName { get; set; }

    [JsonProperty("uuid")]
    public string? UUID { get; set; }
}
