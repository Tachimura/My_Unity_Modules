using UnityEngine;

namespace MapsSystem.MiniMap
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        internal float followDistance = 2.5f;
        [SerializeField]
        internal float elevation = 2.5f;
        [SerializeField]
        internal float angle = 20;
        [SerializeField]
        internal Transform target = null;


        public void Start()
        {
            transform.parent = target.transform;
            transform.localPosition = new Vector3(0, elevation, -followDistance);
            transform.localRotation = Quaternion.Euler(angle, 0, 0);
        }
    }
}
