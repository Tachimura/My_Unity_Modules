using UnityEngine;

namespace TeleportSystem
{
    public class TeleportAccess : InteractableSystem.Interactable
    {
        //DATI CHE FANNO RIFERIMENTO A QUESTA ENTRATA (IN PRATICA IL NOME DELL'ENTRATA)
        [Header("This Teleport Data")]
        [SerializeField]
        private TeleportData currentTeleport = new TeleportData();
        public TeleportData CurrentTeleport => currentTeleport;
        //DATO CHE INDICA LA POSIZIONE DEL PLAYER QUANDO ARRIVIAMO A QUESTO TELEPORT
        [SerializeField]
        private Vector3 playerSpawnPoint = new Vector3();
        public Vector3 PlayerSpawnPoint => playerSpawnPoint;

        //DATI CHE FANNO RIFERIMENTO A DOVE ANDIAMO SE USIAMO QUESTA ENTRATA COME USCITA
        [Header("Teleport Destination Data")]
        [SerializeField]
        private TeleportData destinationTeleport = new TeleportData();
        public TeleportData DestinationTeleport => destinationTeleport;
        [SerializeField]
        private string destinationName = "";
        public string DestinationName => destinationName;

        public override void Interact()
        {
            SceneController.Instance.ChangeScene(destinationTeleport);
        }

        public override void StopInteract() { }
    }
}