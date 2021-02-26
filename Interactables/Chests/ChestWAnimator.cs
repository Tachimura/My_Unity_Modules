using UnityEngine;

public class ChestWAnimator : Chest
{
    [SerializeField]
    protected Animator animator;

    protected virtual void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        base.Interact();
        if (animator != null)
            animator.SetBool("ChestStatus", true);
    }

    public override void StopInteract()
    {
        base.StopInteract();
        if (animator != null)
            animator.SetBool("ChestStatus", false);
    }
}
