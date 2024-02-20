using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Qbem.Gpt.Playground.Schemas.Requests;





var prompts = new[]{
        new {role = "system", content="Faça de conta que você é uma atendente de um condominio e necessita cadastrar e consultar dados do usuario conforme a solicitação"},
        new {role = "user", content = "Olá! Meu nome é Alberto Einstenio. Meu apelido é Malakias" },
        new {role = "user", content = "Registre meu apelido!" },
        new {role = "user", content = "Quero saber quais dados possuio com você?" },
        };


var plugins = new[]
    {

        new Tool{
            Function = new FunctionDefinition {
                Name = "consulta_dados_usuario",
                Description = "disponibiliza os dados quando o usuário solicitar `quais sao os meus dados`",
            }
        },new Tool{
            Function = new FunctionDefinition {
                Name = "insert_dados",
                Description = "insere os dados quando o usuário solicitar `insira meus dados`",
                                Parameters = new Parameters {
                    Properties = new Dictionary<string,  Property>{
                        {
                            "nome",
                            new Property{
                                Type = "string",
                                Description = "nome do usuário ou apelido"
                            }
                        },
                        {
                            "idade",
                            new Property{
                                Type = "string",
                                Description = "idade do usuário"
                            }
                        }
                    }
                }
            }
        }
    };

// Request emulation
string responseBody = await GPTCompletition( prompts, plugins);

if (responseBody.Contains("consulta_dados"))
{
    Console.WriteLine("Executa função de consulta de dados");
}

if (responseBody.Contains("insert_dados"))
{
    string pattern = "\"arguments\"\\s*:\\s*\"\\{(.*?)\\}\"";
    Regex regex = new Regex(pattern);

    Match match = regex.Match(responseBody);

    if (!match.Success)
    {
        Console.WriteLine("Erro ao tentar ler as variáveis");
        return;
    }

    string content = match.Groups[1].Value.Replace("\"", "").Replace("\\", "");
    Console.WriteLine($"Dados {content}");
    Console.WriteLine("Executa função de insert de dados");
}


// serviço
static async Task<string> GPTCompletition(object[] prompts, Tool[] plugins)
{

    // Setting
    const string apiKey = "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";
    const string endpoint = "https://api.openai.com/v1/chat/completions";


    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

    var requestData = new
    {
        model = "gpt-3.5-turbo-0125",
        messages = prompts,
        tools = plugins,
        tool_choice = "auto"
    };


    // prepare request
    string json = JsonConvert.SerializeObject(requestData, Formatting.Indented);
    Console.WriteLine(json);

    // request
    var requestBody = new StringContent(json, Encoding.UTF8, "application/json");


    // response
    var response = await httpClient.PostAsync(endpoint, requestBody);
    response.EnsureSuccessStatusCode();

    var responseBody = await response.Content.ReadAsStringAsync();
    return responseBody;
}