using Newtonsoft.Json;

namespace Budget.BuisnessLogic.Models
{
    public class TokenResponse
    {
        [JsonProperty("id_token")]
        public string TokenId { get; set; }
    }
}