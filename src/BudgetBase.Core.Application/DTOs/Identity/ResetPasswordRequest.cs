using System.Text.Json.Serialization;

namespace BudgetBase.Core.Application.DTOs.Identity
{
    public class ResetPasswordRequest
    {
        [JsonPropertyName("userEmail")]
        public string UserEmail { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("newPassword")]
        public string NewPassword { get; set; }
    }
}