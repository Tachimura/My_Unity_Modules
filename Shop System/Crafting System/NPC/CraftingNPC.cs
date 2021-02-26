using UnityEngine;
using InteractableSystem;
using ShopSystem.CraftingSystem;

public class CraftingNPC : Interactable
{
    [SerializeField]
    private GUIManager guiManager = null;
    [SerializeField]
    private GameObject shopGUIPrefab = null;
    [SerializeField]
    private CraftingManager craftingManager = null;

    private void Start()
    {
        if (guiManager == null)
            guiManager = GUIManager.Instance;
    }

    public override void Interact()
    {
        if (!guiManager.IsAMenuOpen && guiManager.LoadShopGUI(shopGUIPrefab, craftingManager))
            guiManager.ShowShopGUI();
    }
}