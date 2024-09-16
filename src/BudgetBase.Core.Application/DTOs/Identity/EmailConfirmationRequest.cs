using System.Text.Json.Serialization;

namespace BudgetBase.Core.Application.DTOs.Identity
{
    public class EmailConfirmationRequest
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}