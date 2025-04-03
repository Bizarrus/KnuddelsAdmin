using Newtonsoft.Json;

namespace KnuddelsAdmin.Protocol.Receive;

class Event {
    public enum Type {
        [StringValue("deviceConnect")]
        DeviceConnected,

        [StringValue("keyUp")]
        Key_UP
    };

    [JsonProperty("event")]
    public string? EventName { get; set; }

    [JsonProperty("device")]
    public string? Device { get; set; }
}
