using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UtilityInterface;

public class DescribablePanelController : MonoBehaviour
{
    public IDescribable CurrentDescribable { get; private set; } = null;
    [SerializeField]
    private TextMeshProUGUI textName = null;
    [SerializeField]
    private TextMeshProUGUI textDescription = null;
    [SerializeField]
    private Image icon = null;

    public void SetDescription(IDescribable describable)
    {
        if (describable != null)
        {
            textName.text = describable.DescribableName;
            textDescription.text = describable.DescribableDescription;
            icon.sprite = describable.DescribableIcon;
            icon.color = Color.white;
            //
            CurrentDescribable = describable;
        }
        else
        {
            textName.text = "";
            textDescription.text = "";
            icon.sprite = null;
            icon.color = Color.clear;
            //
            CurrentDescribable = null;
        }
    }
}
