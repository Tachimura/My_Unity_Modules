using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuDataCanvasController : PanelController
{
    [Header("Main Menu Tabs")]
    [SerializeField]
    private Image tabPlayerStats = null;
    [SerializeField]
    private Image tabInventory = null;
    [SerializeField]
    private Image tabSomething2 = null;
    [SerializeField]
    private Image tabSettings = null;

    [Header("Main Menu Panels Controller")]
    [SerializeField]
    private PlayerDataGUI panelPlayerStats = null;
    [SerializeField]
    private InventoryCanvasController panelInventory = null;
    [SerializeField]
    private PanelController panelSomething2 = null;
    [SerializeField]
    private PanelController panelSettings = null;

    //PANEL ATTUALMENTE SELEZIONATO
    private PanelController currentPanel = null;
    public PlayerMenuTab CurrentPosition { get; private set; } = PlayerMenuTab.PlayerStats;
    private const PlayerMenuTab minPosition = PlayerMenuTab.PlayerStats;
    private const PlayerMenuTab maxPosition = PlayerMenuTab.Settings;

    protected override void Start()
    {
        base.Start();
        currentPanel = panelPlayerStats;
    }

    public void LoadPlayerDataInGUI(PlayerDataWrapper playerDataWrapper)
    {
        panelInventory.LoadPlayerInventoryInGUI(playerDataWrapper);
        panelPlayerStats.LoadPlayerDataInGUI(playerDataWrapper);
    }

    private void Move(float movement)
    {
        //NASCONDO LA SELEZIONE DEL VECCHIO TAB
        HideCurrentPanel();
        //SE HO DETTO DI ANDARE A DESTRA
        if (movement > 0)
        {
            //GESTISCO IL MOVIMENTO A DESTRA INCREMENTANDO DI UNO
            if (CurrentPosition < maxPosition)
                CurrentPosition += 1;
            //SE ERO ALLA FINE VADO A QUELLO PIù A SINISTRA
            else
                CurrentPosition = minPosition;
        }
        //ALTRIMENTI SE HO DETTO DI ANDARE A SINISTRA
        else if (movement < 0)
        {
            //GESTISCO IL MOVIMENTO A SINISTRA DECREMENTANDO DI UNO
            if (CurrentPosition > minPosition)
                CurrentPosition -= 1;
            //SE ERO ALL'INIZIO VADO A QUELLO PIù A DESTRA
            else
                CurrentPosition = maxPosition;
        }
        //MOSTRO LA SELEZIONE DEL NUOVO TAB
        ShowCurrentPanel();
    }

    private void HideCurrentPanel() => TogglePanelVisibility(false);
    private void ShowCurrentPanel() => TogglePanelVisibility(true);

    private void TogglePanelVisibility(bool visibilityStatus)
    {
        //CAMBIO LA VISIBILITà DELL'ATTUALE TAB NEL MAIN MENU
        switch (CurrentPosition)
        {
            case PlayerMenuTab.PlayerStats:
                {
                    tabPlayerStats.enabled = visibilityStatus;
                    currentPanel = panelPlayerStats;
                    break;
                }
            case PlayerMenuTab.Inventory:
                {
                    tabInventory.enabled = visibilityStatus;
                    currentPanel = panelInventory;
                    break;
                }
            case PlayerMenuTab.Something2:
                {
                    tabSomething2.enabled = visibilityStatus;
                    currentPanel = panelSomething2;
                    break;
                }
            case PlayerMenuTab.Settings:
                {
                    tabSettings.enabled = visibilityStatus;
                    currentPanel = panelSettings;
                    break;
                }
        }
        if (visibilityStatus)
            currentPanel.ShowPanel();
        else
            currentPanel.HidePanel();
    }

    public override PanelControllerReturn OnTabMovement(float movement)
    {
        //GESTIONE MOVIMENTO DEI TABS DEL SUB MENU
        if (currentPanel.OnTabMovement(movement) != PanelControllerReturn.None)
            return PanelControllerReturn.Child;
        //GESTIONE MIA
        //SE NON MI SONO SPOSTATO NEI TABS DEL SUBMENU ALLORA MI SPOSTO NEL MENU ATTUALE
        Move(movement);
        return PanelControllerReturn.Parent;
    }

    public override PanelControllerReturn OnPanelMovement(Vector2 movement)
    {
        //GESTIONE MOVIMENTO DEL SUB MENU
        if (currentPanel.OnPanelMovement(movement) != PanelControllerReturn.None)
            return PanelControllerReturn.Child;
        //GESTIONE MIA
        return PanelControllerReturn.None;
    }

    public override PanelControllerReturn OnActionPressed()
    {
        //GESTIONE ACTION NEL SUB MENU
        if (currentPanel.OnActionPressed() != PanelControllerReturn.None)
            return PanelControllerReturn.Child;
        //GESTIONE MIA
        return PanelControllerReturn.None;
    }

    public override PanelControllerReturn OnBackPressed()
    {
        //GESTIONE BACK NEL SUB MENU
        if (currentPanel.OnBackPressed() != PanelControllerReturn.None)
            return PanelControllerReturn.Child;
        //GESTIONE MIA
        HidePanel();
        return PanelControllerReturn.Parent;
    }
}