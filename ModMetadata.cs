using SPTarkov.Server.Core.Models.Spt.Mod;
using Range = SemanticVersioning.Range;
using Version = SemanticVersioning.Version;

namespace jeroFoodDrink1x1;

public record ModMetadata : IModMetadata
{
    public string ModGuid { get; init; } = "com.jero.fooddrink1x1";
    public string Name { get; init; } = "FoodDrink1x1";
    public string Author { get; init; } = "jero";
    public List<string>? Contributors { get; init; }
    public Version Version { get; init; } = new("2.0.0");
    public Range SptVersion { get; init; } = new("~4.1.6");
    public List<string>? Incompatibilities { get; init; }
    public Dictionary<string, Range>? ModDependencies { get; init; }
    public string? Url { get; init; }
    public string License { get; init; } = "MIT";
    public bool HasPrepatcher { get; init; }
}
