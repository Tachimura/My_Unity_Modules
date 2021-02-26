using System;

namespace TeleportSystem
{
    [Serializable]
    public struct TeleportData
    {
        //L'INDEX DELLA SCENA A CUI FACCIO RIFERIMENTO
        public SceneIndexes SceneIndex;
        //IL MIO NUMERO IDENTIFICATIVO
        public int SceneTeleport;
    }
}