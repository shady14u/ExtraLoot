//plugin.merge -c -m -p ./merge.json

using System.Collections.Generic;

namespace Oxide.Plugins
{
    //Define:FileOrder=1
    [Info("Extra Loot", "Shady14u", "1.0.5")]
    [Description("Add extra items (including custom) to any loot container in the game")]
    public partial class ExtraLoot : RustPlugin
    {
        private void SpawnLoot(StorageContainer container)
        {
            List<BaseItem> items;
            if (!config.Containers.TryGetValue(container.ShortPrefabName, out items))
            {
                return;
            }

            foreach (var value in items)
            {
                if (!(value.Chance >= UnityEngine.Random.Range(0f, 100f))) continue;

                var amount = Core.Random.Range(value.AmountMin, value.AmountMax + 1);
                var shortName = value.IsBlueprint ? "blueprintbase" : value.ShortName;
                var item = ItemManager.CreateByName(shortName, amount, value.SkinId);
                if (item == null) continue;
                    
                item.name = value.DisplayName;
                item.blueprintTarget = value.IsBlueprint ? ItemManager.FindItemDefinition(value.ShortName).itemid : 0;
                container.inventory.capacity++;
                item.MoveToContainer(container.inventory);
            }
        }
    }
}
