using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MapsSystem.MiniMap
{

    public class MiniMap : MonoBehaviour
    {
        public static MiniMap Instance { get; private set; } = null;
        [SerializeField]
        private Terrain Terrain = null;
        [SerializeField]
        private RectTransform scrollViewRectTransform = null;
        [SerializeField]
        private RectTransform contentRectTransform = null;
        [SerializeField]
        private MiniMapIcon miniMapIconPrefab = null;
        [SerializeField]
        private Image mapImage = null;

        private Vector2 mapDimension = Vector2.zero;
        private Matrix4x4 transformationMatrix = Matrix4x4.zero;

        private readonly Dictionary<MiniMapWorldObject, MiniMapIcon> MiniMapObjectLookup = new Dictionary<MiniMapWorldObject, MiniMapIcon>();


        public void Awake() => Instance = this;
        public void Update() => UpdateMiniMapsIcons();

        public void RegisterMiniMapWorldObject(MiniMapWorldObject miniMapWorldObject)
        {
            var miniMapIcon = Instantiate(miniMapIconPrefab);
            miniMapIcon.transform.SetParent(contentRectTransform);
            miniMapIcon.SetIcon(miniMapWorldObject.Icon);
            miniMapIcon.SetColor(miniMapWorldObject.IconColor);
            //MiniMapObjectLookup[miniMapWorldObject] = miniMapIcon;
            MiniMapObjectLookup.Add(miniMapWorldObject, miniMapIcon);
        }

        //DA TESTARE
        public void UnRegisterMiniMapWorldObject(MiniMapWorldObject miniMapWorldObject)
        {
            var miniMapIcon = MiniMapObjectLookup[miniMapWorldObject];
            MiniMapObjectLookup.Remove(miniMapWorldObject);
            //QUANDO CHIUDI OGNI TANTO DA NULL POINTER
            if(miniMapIcon != null)
                Destroy(miniMapIcon.gameObject); //lo creato io,non unity, quindi lo distruggo io muahahahah
        }

        public void UpdateMiniMapsIcons()
        {
            foreach (var keyValuePair in MiniMapObjectLookup)
            {
                var miniMapObject = keyValuePair.Key;
                var miniMapIcon = keyValuePair.Value;

                var mapPosition = WorldPositionToMapPosition(miniMapObject.transform.position);
                //Debug.Log("Posizione player minimappa: " + mapPosition);
                miniMapIcon.RectTransform.anchoredPosition = mapPosition;
                var rotation = miniMapObject.transform.rotation.eulerAngles;
                miniMapIcon.IconRectTransform.localRotation = Quaternion.AngleAxis(-rotation.y, Vector3.forward); //prova con rotation.y positivo
            }
        }

        public Vector2 WorldPositionToMapPosition(Vector3 worldPos)
        {
            var pos = new Vector2(worldPos.x, worldPos.z);
            //Debug.Log("worldPos: " + pos);
            return transformationMatrix.MultiplyPoint3x4(pos);
        }

        public void CalculateTransformationMatrix()
        {
            var miniMapDimensions = contentRectTransform.rect.size;
            // var terrainDimensions = new Vector2(Terrain.terrainData.size.x, Terrain.terrainData.size.z);

            var terrainDimensions = new Vector2(mapDimension.x, mapDimension.y);
            var scaleRatio = miniMapDimensions / terrainDimensions;
            var translation = -miniMapDimensions / 2;

            transformationMatrix = Matrix4x4.TRS(translation, Quaternion.identity, scaleRatio);

            // {scaleRatio.x,   0           ,   0   ,translation.x},
            // {    0       ,   scaleRatio.y,   0   ,translation.y},
            // {    0       ,   0           ,   0   ,   0         },
            // {    0       ,   0           ,   0   ,   0         };
        }

        //DA TESTARE
        private void SetMapImage(Sprite MapImage) => mapImage.sprite = MapImage;

        public void LoadMinimap(Sprite minimapImage, Vector2 minimapDimension)
        {
            //OLD
            /*Debug.Log("LA SCENA é STATA CAMBIATA!!!!!");
            SceneController sceneController = SceneController.Instance; //in teoria esisti
            SetMapImage(sceneController.MinimapImage);
            mapDimension.x = Math.Abs(sceneController.MapStartingPoint.x - sceneController.MapEndingPoint.y);
            mapDimension.y = Math.Abs(sceneController.MapStartingPoint.y - sceneController.MapEndingPoint.x);
            */

            //NEW---------------------------
            SetMapImage(minimapImage);
            mapDimension = minimapDimension;
            //------------------------------

            Debug.Log("mapDimension: " + mapDimension);
            CalculateTransformationMatrix();
        }
    }

}