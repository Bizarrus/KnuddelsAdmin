using Newtonsoft.Json;

namespace KnuddelsAdmin.Protocol;

class Path {
    [JsonProperty("column")]
    public int? Colum { get; set; }

    [JsonProperty("row")]
    public int? Row { get; set; }
}
