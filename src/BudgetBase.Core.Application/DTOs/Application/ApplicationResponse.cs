using System.Text.Json.Serialization;

namespace BudgetBase.Core.Application.DTOs.Application
{
    public class ApplicationResponse
    {
        [JsonPropertyName("succeeded")]
        public bool Succeeded { get; set; }

        [JsonPropertyName("errors")]
        public Dictionary<string, string> Errors { get; set; }
    }
}
