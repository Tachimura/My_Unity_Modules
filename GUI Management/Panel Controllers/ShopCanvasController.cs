using InventorySystem.Items;
using ShopSystem;
using UnityEngine;

public class ShopCanvasController : PanelController
{
    //MANAGER GRAFICO ATTUALE DELLO SHOP APERTO
    private AShopGUI shopGUI = null;
    //GAMEOBJECT CHE CONTIENE IL MANAGER GRAFICO DELLO SHOP
    private GameObject shopGO = null;

    //PREFAB CONTIENE L'OGGETTO GRAFICO DA ISTANZIARE
    //LO SHOP VERRA INSERITO NEL PREFAB ISTANZIATO
    public void LoadShopInGUI(GameObject shopPrefab, IShop shop)
    {
        //SE IL PREFAB PASSATO NON HA UN MANAGER GRAFICO PER LO SHOP NON FACCIO NULLA
        if(shopPrefab.GetComponent<AShopGUI>() != null)
        {
            //SE HO GIà UN QUALCOSA LO RIMUOVO
            if (shopGO != null)
            {
                shopGUI = null;
                Destroy(shopGO);
            }
            //ISTANZO IL NUOVO SHOP
            shopGO = Instantiate(shopPrefab, Vector3.zero, transform.rotation);
            shopGO.transform.SetParent(transform, false);
            //PRENDO LE COMPONENTI E CI CARICO LO SHOP
            shopGUI = shopGO.GetComponent<AShopGUI>();
            shopGUI.LoadShop(shop);
        }
    }

    public override void HidePanel()
    {
        base.HidePanel();
        if (shopGO != null)
        {
            shopGUI = null;
            Destroy(shopGO);
        }
    }

    public override PanelControllerReturn OnActionPressed()
    {
        //TODO: USARE RISORSE
        if (shopGUI != null)
        {
            Item item = shopGUI.AcquireSelectedItem();
            if (item != null)
            {
                Debug.Log("ShopCanvasController: Player Acquired Item: " + item.DescribableName);
            }
        }
        return PanelControllerReturn.Parent;
    }
    
    public override PanelControllerReturn OnPanelMovement(Vector2 movement)
    {
        if (shopGUI != null)
        {
            shopGUI.Move(movement);
            return PanelControllerReturn.Child;
        }
        return PanelControllerReturn.Parent;
    }

    public override PanelControllerReturn OnBackPressed() => PanelControllerReturn.None;
    public override PanelControllerReturn OnTabMovement(float movement) => PanelControllerReturn.None;
}