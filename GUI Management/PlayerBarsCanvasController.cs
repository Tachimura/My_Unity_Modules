using TMPro;
using UnityEngine;

public class PlayerBarsCanvasController : MonoBehaviour
{
    [SerializeField]
    private ProgressBar hpBar = null;
    [SerializeField]
    private TextMeshProUGUI hpLabel = null;
    [SerializeField]
    private ProgressBar mpBar = null;
    [SerializeField]
    private TextMeshProUGUI mpLabel = null;
    [SerializeField]
    private ProgressBar expBar = null;
    [SerializeField]
    private TextMeshProUGUI expLabel = null;
    [SerializeField]
    private ProgressBar staminaBar = null;

    private PlayerDataWrapper playerDataWrapper;

    public void LoadPlayerData(PlayerDataWrapper playerDataWrapper)
    {
        //RESETTO LA PLAYER DATA SE è IMPOSTATA
        ResetPlayerData();
        //MI COLLEGO AL PLAYER DATA WRAPPER NUOVO
        this.playerDataWrapper = playerDataWrapper;
        playerDataWrapper.OnPlayerHPChanged += UpdateHPStatus;
        playerDataWrapper.OnPlayerMPChanged += UpdateMPStatus;
        playerDataWrapper.OnPlayerExpChanged += UpdateExpStatus;
        playerDataWrapper.OnPlayerStaminaChanged += UpdateStaminaStatus;
        //AGGIORNO LA GUI AI VALORI BASE
        UpdateHPStatus(playerDataWrapper.PlayerHP, playerDataWrapper.PlayerMaxHP);
        UpdateMPStatus(playerDataWrapper.PlayerMP, playerDataWrapper.PlayerMaxMP);
        UpdateExpStatus(playerDataWrapper.PlayerExp, playerDataWrapper.PlayerMaxExp, playerDataWrapper.PlayerLevel);
    }
    private void ResetPlayerData()
    {
        if (playerDataWrapper != null)
        {
            //MI SCOLLEGO DAI LISTENER
            playerDataWrapper.OnPlayerHPChanged -= UpdateHPStatus;
            playerDataWrapper.OnPlayerMPChanged -= UpdateMPStatus;
            playerDataWrapper.OnPlayerExpChanged -= UpdateExpStatus;
            playerDataWrapper.OnPlayerStaminaChanged -= UpdateStaminaStatus;
            playerDataWrapper = null;
        }
    }

    private void OnDestroy() => ResetPlayerData();

    public void UpdateHPStatus(int currentValue, int maxValue)
    {
        if (hpLabel != null)
            hpLabel.text = "HP: " + currentValue + "/" + maxValue;
        if (hpBar != null)
            hpBar.UpdateGUI(currentValue, maxValue);
    }

    public void UpdateMPStatus(int currentValue, int maxValue)
    {
        if (mpLabel != null)
            mpLabel.text = "MP: " + currentValue + "/" + maxValue;
        if (mpBar != null)
            mpBar.UpdateGUI(currentValue, maxValue);
    }
    public void UpdateExpStatus(int currentValue, int maxValue, int currentLevel)
    {
        if (expLabel != null)
            expLabel.text = currentLevel.ToString();
        if (expBar != null)
            expBar.UpdateGUI(currentValue, maxValue);
    }

    public void UpdateStaminaStatus(int currentValue, int maxValue)
    {
        if (staminaBar != null)
        {
            staminaBar.gameObject.SetActive(true);
            staminaBar.UpdateGUI(currentValue, maxValue);
            if (currentValue == maxValue)
                staminaBar.gameObject.SetActive(false);
        }
    }
}