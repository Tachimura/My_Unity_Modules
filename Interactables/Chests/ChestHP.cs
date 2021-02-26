using UnityEngine;

public class ChestHP : ChestWText
{
    //TODO DA GENERALIZZARE ANCHE SE è DI TEST
    [SerializeField]
    private PlayerDataManager player;
    [SerializeField]
    [Range(-5, 5)]
    private int hpModify = 1;

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
            player.PlayerDataWrapper.PlayerHP += hpModify;
    }

    public override void StopInteract()
    {
        base.StopInteract();
    }
}