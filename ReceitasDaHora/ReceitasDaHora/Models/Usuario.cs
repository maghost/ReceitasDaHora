using Newtonsoft.Json;

namespace ReceitasDaHora.Models
{

    [JsonObject("User")]
    public class Usuario
    {
        [JsonProperty("userId")]
        public string Id { get; set; }
    }
}
