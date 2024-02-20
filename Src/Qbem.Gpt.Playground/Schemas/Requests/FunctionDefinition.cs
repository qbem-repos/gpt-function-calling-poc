using Newtonsoft.Json;

namespace Qbem.Gpt.Playground.Schemas.Requests;
public class FunctionDefinition
{
    [JsonProperty("name")]
    public required string Name { get; set; }

    [JsonProperty("description")]
    public required string Description { get; set; }

    [JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
    public Parameters? Parameters { get; set; } = null;
}