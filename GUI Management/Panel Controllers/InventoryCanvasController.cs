using InventorySystem.Items;
using InventorySystem.Player;
using UnityEngine;
using UtilityInterface;

public class InventoryCanvasController : PanelController
{
    [SerializeField]
    private PlayerInventoryGUI playerInventoryGUI = null;
    //MOVIMENTO NEL PANNELLO
    public override PanelControllerReturn OnPanelMovement(Vector2 movement)
    {
        playerInventoryGUI.Move(movement);
        return PanelControllerReturn.Parent;
    }
    //MOVIMENTO NEI TAB, RITORNO NONE XKè X ORA NON CI SONO TAB
    public override PanelControllerReturn OnTabMovement(float movement) => PanelControllerReturn.None;
    //GESTIONE PRESSIONE ACTION
    public override PanelControllerReturn OnActionPressed()
    {
        bool used = false;
        Item item = playerInventoryGUI.PeekSelected();
        if (item != null)
        {
            if (item is IUseable)
            {
                if ((item as IUseable).CanUse())
                {
                    item = playerInventoryGUI.RetrieveSelected();
                    (item as IUseable).Use();
                    Destroy(item);
                    used = true;
                }
                else
                    used = false;
            }
            else if (item is Equipment)
            {
                item = playerInventoryGUI.RetrieveSelected();
                PlayerDataManager.Instance.PlayerDataWrapper.Equip(item as Equipment);
                used = true;
            }
            else
                used = false;
        }
        //TODO FARNE QUALCOSA DI QUESTO VALORE DI RITORNO CHE DICE SE HO USATO O MENO L'ITEM
        //if(used) {} else {}
        return PanelControllerReturn.Parent;
    }
    //GESTIONE PRESSIONE BACK, NON CI SONO SUB-MENU E DUNQUE RITORNO NONE
    public override PanelControllerReturn OnBackPressed() => PanelControllerReturn.None;

    public void LoadPlayerInventoryInGUI(PlayerDataWrapper playerDataWrapper)
         => playerInventoryGUI.LoadInventory(playerDataWrapper);
}