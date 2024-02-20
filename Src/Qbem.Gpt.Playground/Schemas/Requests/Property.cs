using Newtonsoft.Json;

namespace Qbem.Gpt.Playground.Schemas.Requests;
public class Property
{
    [JsonProperty("type")]
    public required string Type { get; set; }

    [JsonProperty("description")]
    public required string Description { get; set; }

    [JsonProperty("enum", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? Enum { get; set; } = null;
}