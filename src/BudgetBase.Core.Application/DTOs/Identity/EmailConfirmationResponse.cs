using System.Text.Json.Serialization;
using BudgetBase.Core.Application.DTOs.Application;

namespace BudgetBase.Core.Application.DTOs.Identity
{
    public class TokenResponse : ApplicationResponse
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}