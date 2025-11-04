using System.Text.Json.Serialization;

namespace jeroFoodDrink1x1;

public class ExcludeConfig
{
    [JsonPropertyName("ExcludeItemIds")]
    public List<string> ExcludeItemIds { get; set; } = new List<string>();
}

