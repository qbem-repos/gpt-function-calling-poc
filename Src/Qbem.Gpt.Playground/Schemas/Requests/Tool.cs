using Newtonsoft.Json;

namespace Qbem.Gpt.Playground.Schemas.Requests;

public class Tool 
{
    [JsonProperty("type")]
    public string Type { get; } = "function";

    [JsonProperty("function")]
    public required FunctionDefinition Function { get; set; }
}