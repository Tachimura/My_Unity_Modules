using TMPro;
using UnityEngine;

public class ChestWText : Chest
{
    [SerializeField]
    protected TextMeshPro chestText;
    [Header("Chest Texts")]
    [SerializeField]
    protected string baseText = "Base Text";
    [SerializeField]
    protected string interactingText = "Interacting Text";

    protected virtual void Start()
    {
        if (chestText == null)
            chestText = GetComponentInChildren<TextMeshPro>();
        if (chestText != null)
            chestText.text = baseText;
    }

    public override void Interact()
    {
        base.Interact();
        if (chestText != null)
            chestText.text = interactingText;
    }

    public override void StopInteract()
    {
        base.StopInteract();
        if (chestText != null)
            chestText.text = baseText;
    }
}
