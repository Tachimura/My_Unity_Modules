using UnityEngine;

public class ChestMP : ChestWText
{
    //TODO DA GENERALIZZARE ANCHE SE è DI TEST
    [SerializeField]
    private PlayerDataManager player;
    [SerializeField]
    [Range(-5, 5)]
    private int mpModify = 1;
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
            player.PlayerDataWrapper.PlayerMP += mpModify;
    }

    public override void StopInteract()
    {
        base.StopInteract();
    }
}