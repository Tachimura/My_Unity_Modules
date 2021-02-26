using UnityEngine;

public class ChestEXP : ChestWText
{
    //TODO DA GENERALIZZARE ANCHE SE è DI TEST
    [SerializeField]
    private PlayerDataManager player;
    [SerializeField]
    [Range(-5, 20)]
    private int expModify = 1;
    protected override void Start()
    {
        base.Start();
        if (player == null)
            player = PlayerDataManager.Instance;
    }

    public override void Interact()
    {
        base.Interact();
        if (player != null)
            player.PlayerDataWrapper.GainExp(expModify);
    }

    public override void StopInteract()
    {
        base.StopInteract();
    }
}