using UnityEngine;

public class Chest : InteractableSystem.Interactable
{
    public override void Interact()
    {
        Debug.Log("Chest: Interact");
    }

    public override void StopInteract()
    {
        Debug.Log("Chest: StopInteract");
    }
}