using TMPro;
using UnityEngine;

public class FloatingNameText : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro floatingNameText = null;
    private static Transform playerCamera = null;

    // Start is called before the first frame update
    private void Start()
    {
        if (floatingNameText == null)
            floatingNameText = GetComponent<TextMeshPro>();
        if (playerCamera == null)
            playerCamera = Camera.main.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if(floatingNameText != null && playerCamera != null)
        {
            //GUARDO VERSO LA CAMERA
            floatingNameText.transform.LookAt(playerCamera.position);
            //GIRO DI 180 XKè LE NORMALI SONO AL CONTRARIO
            floatingNameText.transform.Rotate(0, 180, 0);
        }
    }
}