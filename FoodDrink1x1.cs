using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Servers;
using System.Reflection;
using Path = System.IO.Path;

namespace jeroFoodDrink1x1;

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 10)]
public class FoodDrink1x1 : IOnLoad
{
    private readonly ISptLogger<FoodDrink1x1> _logger;
    private readonly DatabaseServer _databaseServer;
    private readonly ModConfig _config;
    private readonly HashSet<string> _targetParentIds;

    public FoodDrink1x1(ISptLogger<FoodDrink1x1> logger, DatabaseServer databaseServer, ModHelper modHelper)
    {
        _logger = logger;
        _databaseServer = databaseServer;

        string? modPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (string.IsNullOrEmpty(modPath))
        {
            _logger.Error("[JERO] ERROR: Could not determine mod path. The mod will not make any changes.");
            _config = new ModConfig();
            _targetParentIds = new HashSet<string>();
            return;
        }

        _config = modHelper.GetJsonDataFromFile<ModConfig>(modPath, Path.Join("config", "config.json")) ?? new ModConfig();

        if (_config.TargetParentIds.Count == 0)
        {
            _logger.Warning("[JERO] WARNING: config.json not found or empty. The mod will not make any changes.");
        }
        else
        {
            _logger.Info("[JERO] config.json loaded successfully.");
        }

        _targetParentIds = new HashSet<string>(_config.TargetParentIds);
    }

    public Task OnLoad()
    {
        var itemsDb = _databaseServer.GetTables().Templates.Items;
        int itemsAdjusted = 0;

        foreach (var item in itemsDb.Values.Where(item => _targetParentIds.Contains(item.Parent)))
        {
            if (TryResizeItem(item))
            {
                itemsAdjusted++;
            }
        }

        _logger.Info($"[JERO] Drink & Food 1x1, {itemsAdjusted} adjustments made!");
        return Task.CompletedTask;
    }

    private bool TryResizeItem(TemplateItem item)
    {
        if (item.Properties is null)
        {
            return false;
        }

        if (item.Properties.Width <= 1 && item.Properties.Height <= 1)
        {
            return false;
        }

        item.Properties.Width = 1;
        item.Properties.Height = 1;
        return true;
    }
}
