namespace Oxide.Plugins
{
    //Define:FileOrder=60
    public partial class ExtraLoot
    {

        private void OnLootSpawn(StorageContainer container)
        {
            NextTick(() => { SpawnLoot(container); });
        }

    }
}
