using Newtonsoft.Json;

namespace KnuddelsAdmin.Protocol;

class Dimension {
    [JsonProperty("columns")]
    public int? Columns { get; set; }

    [JsonProperty("rows")]
    public int? Rows { get; set; }
}
