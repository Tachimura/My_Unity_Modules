using UnityEngine;
namespace InteractableSystem
{
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private InteractableType interactableType = InteractableType.Active;
        public InteractableType InteractableType => interactableType;

        public virtual void Interact() { }

        public virtual void StopInteract() { }
    }
}