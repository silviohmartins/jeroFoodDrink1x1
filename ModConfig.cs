using System.Text.Json.Serialization;

namespace jeroFoodDrink1x1;

public class ModConfig
{
    // O nome da propriedade (TargetParentIds) deve ser exatamente
    // igual à chave que você definiu no arquivo config.json.
    [JsonPropertyName("TargetParentIds")]
    public List<string> TargetParentIds { get; set; } = new List<string>();
}