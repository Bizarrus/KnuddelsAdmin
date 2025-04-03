using Newtonsoft.Json;

namespace KnuddelsAdmin.Protocol.Receive;

class DeviceConnected : Event {
    [JsonProperty("deviceInfo")]
    public DeviceInfo? Info { get; set; }
}
