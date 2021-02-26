namespace TeleportSystem
{
    //PUò TORNARE UTILE SE SI VUOLE TIRARE FUORI UN CERTO TELEPORT
    public static class SceneDictionary
    {
        //LISTA DI TELEPORT DEL GIOCO
        public static readonly TeleportData TEST_MAP_ENTRANCE_0 = new TeleportData() { SceneIndex = SceneIndexes.TEST_SHOP_MAP, SceneTeleport = 0 };
        public static readonly TeleportData TEST_MAP_ENTRANCE_1 = new TeleportData() { SceneIndex = SceneIndexes.TEST_SHOP_MAP, SceneTeleport = 1 };
        public static readonly TeleportData TEST_MAP_MONSTERS_ENTRANCE_0 = new TeleportData() { SceneIndex = SceneIndexes.TEST_MAP_MONSTERS, SceneTeleport = 0 };
    }
}