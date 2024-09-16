using System.Text.Json.Serialization;

namespace BudgetBase.Core.Application.DTOs.Identity
{
    public class SignUpRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}