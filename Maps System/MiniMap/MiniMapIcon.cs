using UnityEngine;
using UnityEngine.UI;

namespace MapsSystem.MiniMap
{
    /**
     * Classe che rappresenta l'icona presente sulla minimappa
     **/
    public class MiniMapIcon : MonoBehaviour
    {
        public Image Icon;
        public RectTransform RectTransform;
        public RectTransform IconRectTransform;

        public void SetIcon(Sprite icon) => Icon.sprite = icon;
        
        public void SetColor(Color color) => Icon.color = color;

        public void SetWidth(int width) => RectTransform.rect.Set(RectTransform.rect.x, RectTransform.rect.y , width, RectTransform.rect.height);
        
        public void SetHeight(int height) => RectTransform.rect.Set(RectTransform.rect.x, RectTransform.rect.y, RectTransform.rect.width, height);

        public void SetSize(int width, int height) => RectTransform.rect.Set(RectTransform.rect.x, RectTransform.rect.y, width, height);


    }
}