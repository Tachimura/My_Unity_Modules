using UnityEngine;

namespace MapsSystem.MiniMap
{
    /**
     * Classe che rappresenta un Game object presenta sulla mappa
     **/
    public class MiniMapWorldObject : MonoBehaviour
    {
        public Sprite Icon;
        public Color IconColor = Color.white;

        public void Start()
        {
            MiniMap.Instance.RegisterMiniMapWorldObject(this);
        }

        public void OnDestroy()
        {
            MiniMap.Instance.UnRegisterMiniMapWorldObject(this);
        }
    }
}