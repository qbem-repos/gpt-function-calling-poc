using Newtonsoft.Json;

namespace Qbem.Gpt.Playground.Schemas.Requests;

public class Parameters
{
    [JsonProperty("type")]
    public string Type { get; } = "object";

    [JsonProperty("properties")]
    public Dictionary<string, Property>? Properties { get; set; } = null;
}
