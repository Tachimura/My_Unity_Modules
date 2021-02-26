using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProgressBar : MonoBehaviour
{
    //CREO LA POSSIBILITà DI CREARE UN OGGETTO DI QUESTO TIPO DIRETTAMENTE DALLA SCENA IN UNITY
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Progress Bar/Linear Progress Bar")]
    public static void AddLinearProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
    [MenuItem("GameObject/UI/Progress Bar/Radial Progress Bar")]
    public static void AddRadialProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Radial Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif
    [SerializeField]
    private Image fill = null;
    [SerializeField]
    private Color fillColor = Color.white;
    [SerializeField]
    private bool shrinkBar = false;
    [SerializeField]
    private Image shrinkFill = null;
    [SerializeField]
    private float shrinkMaxTimer = 2f;
    [SerializeField]
    private float shrinkSpeed = 1f;
    private float shrinkTimer = 0f;

    private void Start()
    {
        if (shrinkBar)
            shrinkFill.fillAmount = 0f;
    }

    private void Update()
    {
        if (shrinkBar)
        {
            if (shrinkTimer > 0)
                shrinkTimer -= Time.deltaTime;
            else if (shrinkFill.fillAmount > fill.fillAmount)
                shrinkFill.fillAmount -= shrinkSpeed * Time.deltaTime;
        }
    }

    public void UpdateGUI(int currentValue, int maximumValue)
    {
        float fillAmount = (float)currentValue / maximumValue;
        //se ho subito danni faccio partire timer, altrimenti reimposto lo shrinkFill
        if (shrinkBar)
        {
            shrinkFill.fillAmount = fill.fillAmount;
            if (shrinkFill.fillAmount > fillAmount)
                shrinkTimer = shrinkMaxTimer;
        }
        fill.fillAmount = fillAmount;
        fill.color = fillColor;
    }
}