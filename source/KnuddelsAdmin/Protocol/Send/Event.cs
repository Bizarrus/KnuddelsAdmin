using Newtonsoft.Json;

namespace KnuddelsAdmin.Protocol.Send;

class Event {
    [JsonProperty("event")]
    public string? EventName { get; set; }

    [JsonProperty("context")]
    public string? Context { get; set; }
}
