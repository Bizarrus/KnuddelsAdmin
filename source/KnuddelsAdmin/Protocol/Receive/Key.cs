using Newtonsoft.Json;

namespace KnuddelsAdmin.Protocol.Receive;

class Key : Event {
    [JsonProperty("action")]
    public string? Action { get; set; }
    
    [JsonProperty("context")]
    public string? Context { get; set; }

    [JsonProperty("payload")]
    public Payload? Payload { get; set; }
}
