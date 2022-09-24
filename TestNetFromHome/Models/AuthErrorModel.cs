using Newtonsoft.Json;

namespace TestNetFromHome.Models
{
    public class AuthErrorModel
    {
        [JsonProperty("ResultMessage")]
        public string ErrorMessage { get; set; }
    }
}
