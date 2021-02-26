using ShopSystem;
using System;
using UnityEngine;

public class GameCanvasController : PanelController
{
    public static GameCanvasController Instance { get; private set; }

    [SerializeField]
    private PlayerInfoCanvasController playerInfoController = null;
    [SerializeField]
    private PlayerMenuDataCanvasController playerDataController = null;
    [SerializeField]
    private ShopCanvasController shopsController = null;

    //PANEL CONTROLLER ATTUALMENTE ATTIVO
    private PanelController activePanel = null;

    private bool hasAPanelOpen = false;
    public bool HasAPanelOpen
    {
        get => hasAPanelOpen;
        private set
        {
            if (hasAPanelOpen != value)
            {
                hasAPanelOpen = value;
                OnPanelVisibilityChanged?.Invoke(value);
            }
        }
    }
    public event Action<bool> OnPanelVisibilityChanged;

    private void Awake() => Instance = this;

    public override PanelControllerReturn OnActionPressed()
    {
        //SE NON HO ACTIVE PANEL, RITORNO NONE PERCHè NON GESTISCO NULLA
        if (activePanel == null)
            return PanelControllerReturn.None;
        if (activePanel.OnActionPressed() != PanelControllerReturn.None)
            return PanelControllerReturn.Child;

        //GESTIONE MIA
        return PanelControllerReturn.Parent;
    }

    public override PanelControllerReturn OnBackPressed()
    {
        //SE NON HO ACTIVE PANEL, RITORNO NONE PERCHè NON GESTISCO NULLA
        if (activePanel == null)
            return PanelControllerReturn.None;
        //VEDO SE L'ACTIVE PANEL PUò GESTIRE LA RICHIESTA
        if (activePanel.OnBackPressed() != PanelControllerReturn.None)
            return PanelControllerReturn.Child;

        //GESTIONE MIA
        HideMenuTabs();
        return PanelControllerReturn.Parent;
    }

    public override PanelControllerReturn OnPanelMovement(Vector2 movement)
    {
        //SE NON HO ACTIVE PANEL, RITORNO NONE PERCHè NON GESTISCO NULLA
        if (activePanel == null)
            return PanelControllerReturn.None;
        //VEDO SE L'ACTIVE PANEL PUò GESTIRE LA RICHIESTA
        if (activePanel.OnPanelMovement(movement) != PanelControllerReturn.None)
            return PanelControllerReturn.Child;

        //GESTIONE MIA
        return PanelControllerReturn.Parent;
    }

    public override PanelControllerReturn OnTabMovement(float movement)
    {
        //SE NON HO ACTIVE PANEL, RITORNO NONE PERCHè NON GESTISCO NULLA
        if (activePanel == null)
            return PanelControllerReturn.None;
        //VEDO SE L'ACTIVE PANEL PUò GESTIRE LA RICHIESTA
        if (activePanel.OnTabMovement(movement) != PanelControllerReturn.None)
            return PanelControllerReturn.Child;

        //GESTIONE MIA
        return PanelControllerReturn.Parent;
    }

    public void ShowMenuTabs()
    {
        if (activePanel != null)
        {
            activePanel.ShowPanel();
            HasAPanelOpen = true;
        }
    }

    public void HideMenuTabs()
    {
        if (activePanel != null)
        {
            activePanel.HidePanel();
            //TODO CONTROLLARE QUA
            /*
            if (activePanel != playerDataController)
                PlayerModelController.Instance.StopInteract();
            */
            HasAPanelOpen = false;
            activePanel = null;
        }
    }

    public bool SetActivePanel(GameCanvasPanel panel)
    {
        //POSSO ATTIVARE UN NUOVO PANEL SOLO SE NON C'è NE SONO ALTRI ATTIVI
        if (activePanel != null)
            return false;
        switch (panel)
        {
            case GameCanvasPanel.PlayerPanel:
                {
                    activePanel = playerDataController;
                    break;
                }
            case GameCanvasPanel.ShopPanel:
                {
                    activePanel = shopsController;
                    break;
                }
        }
        return true;
    }

    public void LoadPlayerDataInGUI(PlayerDataWrapper playerDataWrapper)
    {
        //PLAYER MAIN MENU TABS
        playerDataController.LoadPlayerDataInGUI(playerDataWrapper);
        //PLAYER BARS HP, MP, EXP, STAMINA
        playerInfoController.LoadPlayerDataInGUI(playerDataWrapper);
    }

    public void UpdateActionInterface(bool active, string actionButton, string actionText)
        => playerInfoController.UpdateActionInterface(active, actionButton, actionText);

    public void LoadShopInGUI(GameObject shopPrefab, IShop shop)
        => shopsController.LoadShopInGUI(shopPrefab, shop);
}