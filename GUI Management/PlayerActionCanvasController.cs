using TMPro;
using UnityEngine;

public class PlayerActionCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject actionContainer = null;
    [SerializeField]
    private TextMeshProUGUI actionTextLabel = null;
    [SerializeField]
    private TextMeshProUGUI actionTextButton = null;

    public void UpdateActionInterface(bool active, string actionButton, string actionText)
    {
        if (actionContainer != null)
        {
            actionContainer.SetActive(active);
            if (active)
            {
                if (actionTextLabel != null)
                    actionTextLabel.text = actionText;
                if (actionTextButton != null)
                    actionTextButton.text = actionButton;
            }
        }
    }
}