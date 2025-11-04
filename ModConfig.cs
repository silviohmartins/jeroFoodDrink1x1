using System.Text.Json.Serialization;

namespace jeroFoodDrink1x1;

public class ModConfig
{
    [JsonPropertyName("TargetParentIds")]
    public List<string> TargetParentIds { get; set; } = new List<string>();
}
