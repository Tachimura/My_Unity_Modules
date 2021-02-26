using UnityEngine;

public class PlayerInfoCanvasController : MonoBehaviour
{
    [SerializeField]
    private PlayerBarsCanvasController playerBarsController = null;
    [SerializeField]
    private PlayerActionCanvasController playerActionController = null;

    public void UpdateActionInterface(bool active, string actionButton, string actionText)
        => playerActionController.UpdateActionInterface(active, actionButton, actionText);

    public void LoadPlayerDataInGUI(PlayerDataWrapper playerDataWrapper)
        => playerBarsController.LoadPlayerData(playerDataWrapper);
}