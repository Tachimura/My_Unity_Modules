using InventorySystem.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.GUI
{
    public class InventoryGUI : MonoBehaviour, IInventoryGUI
    {
        [Header("Gestione Slots")]
        [SerializeField]
        private GameObject slotPrefab = null;
        [SerializeField]
        private Transform slotsContainer = null;
        [SerializeField]
        private GLController gridController = null;
        [SerializeField]
        private DescribablePanelController describablePanel = null;

        private List<InventoryGUISlot> inventorySlots = null;

        public IInventory Inventory { get; private set; } = null;

        public int CurrentSelectedSlotPosition { get; private set; }

        private void Awake()
        {
            CurrentSelectedSlotPosition = -1;
            inventorySlots = new List<InventoryGUISlot>();
        }

        public void Start()
        {
            if (CurrentSelectedSlotPosition == -1)
                ShowItemInfo(null);
            else
                ShowItemInfo(inventorySlots[CurrentSelectedSlotPosition].PeekItem());
        }

        public void LoadInventory(IInventory inventory)
        {
            //SE HO GIà UN INVENTARIO LO RESETTO
            if (Inventory != null)
            {
                //TOLGO IL VECCHIO EVENTO, COSì DA EVITARE PROBLEMI IN MEMORIA
                Inventory.OnInventorySlotStatusChanged -= InventorySlotStatusChanged;
                //RESETTO OGNI SLOT
                foreach (IInventoryGUISlot slot in inventorySlots)
                    slot.Reset();
            }
            //METTO L'INVENTARIO NUOVO
            Inventory = inventory;
            //EVENTO CHE DICE CHE UNO SLOT è STATO CREATO O RIMOSSO
            Inventory.OnInventorySlotStatusChanged += InventorySlotStatusChanged;
            //RESIZE DELL'INVENTARIO
            int sizeDifference = inventory.InventorySize - inventorySlots.Count;
            //SE IL NUOVO INVENTARIO HA PIù ELEMENTI DI QUELLI ATTUALI, QUELLI LI RIMUOVO
            if (sizeDifference < 0)
            {
                //- SIZE PERCHè è NEGATIVO DUNQUE LO GIRO E DIVENTA POSITIVO
                for (int cont = 0; cont < -sizeDifference; cont++)
                {
                    //PRENDO L'ULTIMO ELEMENTO
                    InventoryGUISlot slot = inventorySlots[inventory.InventorySize];
                    //LO RIMUOVO DALLA MIA LISTA E DALLA NAVIGAZIONE
                    inventorySlots.Remove(slot);
                    gridController.RemoveNavigableItem(slot.gameObject.GetComponent<Selectable>());
                    //LO RIMUOVO DAL GAME
                    Destroy(slot.gameObject);
                }
            }
            else if (sizeDifference > 0)
            {
                //DEVO CREARE NUOVI ELEMENTI
                for (int cont = 0; cont < sizeDifference; cont++)
                {
                    //CREO NUOVO SLOT
                    GameObject slotObj = Instantiate(slotPrefab, slotsContainer);
                    slotObj.transform.SetParent(slotsContainer, false);
                    //PRENDO LA COMPONENTE E LA AGGIUNGO ALLA LISTA ATTUALE E ALLA NAVIGAZIONE
                    inventorySlots.Add(slotObj.GetComponent<InventoryGUISlot>());
                    gridController.AddNavigableItem(slotObj.gameObject.GetComponent<Selectable>());
                }
            }
            //COLLEGO GLI SLOT AI LORO DATI
            for (int cont = 0; cont < inventory.InventorySize; cont++)
                inventorySlots[cont].SetInventorySlot(inventory.InventorySlotAtPosition(cont));
            if (inventorySlots.Count > 0)
            {
                CurrentSelectedSlotPosition = 0;
                //IMPOSTO IL PRIMO ELEMENTO COME SELEZIONATO E NE MOSTRO LE INFORMAZIONI
                inventorySlots[CurrentSelectedSlotPosition].Select();
                ShowItemInfo(inventorySlots[CurrentSelectedSlotPosition].PeekItem());
            }
            else
            {
                CurrentSelectedSlotPosition = -1;
                ShowItemInfo(null);
            }
            RecalculateNavigation();
        }

        protected void InventorySlotStatusChanged(IInventorySlot slot, bool state)
        {
            //SE LO SLOT è DIVENTATO ATTIVO
            if (state)
            {
                //MI CREO LO SLOT GRAFICO E LO COLLEGO
                GameObject slotObj = Instantiate(slotPrefab, slotsContainer);
                slotObj.transform.SetParent(slotsContainer, false);
                InventoryGUISlot gSlot = slotObj.GetComponent<InventoryGUISlot>();
                gSlot.SetInventorySlot(slot);
                inventorySlots.Add(gSlot);
                gridController.AddNavigableItem(slotObj.GetComponent<Selectable>());
                //SE PRIMA NON CI STAVANO ALTRI ELEMENTI, REIMPOSTO IL CURRENT A 0
                //ED AGGIORNO LA PARTE GRAFICA
                if (CurrentSelectedSlotPosition == -1)
                {
                    CurrentSelectedSlotPosition = 0;
                    inventorySlots[CurrentSelectedSlotPosition].Select();
                    ShowItemInfo(inventorySlots[CurrentSelectedSlotPosition].PeekItem());
                }
            }
            //SE INVECE LO SLOT NON è PIù UTILIZZATO DEVO RIMUOVERE LO SLOT GRAFICO
            else
            {
                //MI CERCO QUALE DEGLI SLOT GRAFICI HA LO SLOT INDICATO
                int position = 0;
                foreach (InventoryGUISlot iSlot in inventorySlots)
                {
                    if (iSlot.InventorySlot == slot)
                        break;
                    position++;
                }
                //DESELEZIONO, RIMUOVO E DISTRUGGO LO SLOT
                InventoryGUISlot slotToRemove = inventorySlots[position];
                //DESELEZIONO QUELLO ATTUALE PERCHè CAMBIERà
                inventorySlots[CurrentSelectedSlotPosition].Deselect();
                inventorySlots.Remove(slotToRemove);
                gridController.RemoveNavigableItem(slotToRemove.GetComponent<Selectable>());
                Destroy(slotToRemove.gameObject);
                //SE CI SONO ELEMENTI
                if (inventorySlots.Count > 0)
                {
                    //SE LA POSIZIONE ATTUALE FUORIESCE IL LIMITE REIMPOSTO A 0, ALTRIMENTI MANTENGO LA POSIZIONE ATTUALE
                    if (CurrentSelectedSlotPosition >= inventorySlots.Count)
                        CurrentSelectedSlotPosition = 0;
                    InventoryGUISlot slotToShow = inventorySlots[CurrentSelectedSlotPosition];
                    //LO SELEZIONO E MOSTRO I DETTAGLI DELL'OGGETTO CHE CONTIENE
                    slotToShow.Select();
                    ShowItemInfo(slotToShow.PeekItem());
                }
                //IN CASO CONTRARIO NON CI SONO ELEMENTI, IMPOSTO A -1 E VIENE MOSTRATO NIENTE
                else
                {
                    CurrentSelectedSlotPosition = -1;
                    ShowItemInfo(null);
                }
            }
            //RICALCOLO LA NAVIGAZIONE NELL'INVENTARIO
            RecalculateNavigation();
        }

        private void RecalculateNavigation() => gridController.RecalculateNavigation();

        public Item PeekSelected()
        {
            Item item = null;
            if (CurrentSelectedSlotPosition >= 0 && CurrentSelectedSlotPosition < inventorySlots.Count)
                item = inventorySlots[CurrentSelectedSlotPosition].PeekItem();
            return item;
        }

        public Item RetrieveSelected()
        {
            Item item = null;
            if (CurrentSelectedSlotPosition >= 0 && CurrentSelectedSlotPosition < inventorySlots.Count)
                item = inventorySlots[CurrentSelectedSlotPosition].RetrieveItem();
            return item;
        }

        public void Move(Vector2 direction)
        {
            lock (this)
            {
                //SE CI SONO ELEMENTI DA SCORRERE
                if (inventorySlots.Count > 0)
                {
                    Selectable currentSelectedSlot = inventorySlots[CurrentSelectedSlotPosition].GetComponent<Selectable>();
                    Selectable nextSelectedSlot = null;
                    switch (direction.x)
                    {
                        //STO FERMO
                        case 0: { nextSelectedSlot = currentSelectedSlot; break; }
                        //DESTRA
                        case 1: { nextSelectedSlot = (currentSelectedSlot.navigation.selectOnRight != null) ? currentSelectedSlot.navigation.selectOnRight : null; break; }
                        //SINISTRA
                        case -1: { nextSelectedSlot = (currentSelectedSlot.navigation.selectOnLeft != null) ? currentSelectedSlot.navigation.selectOnLeft : null; break; }
                    }
                    switch (direction.y)
                    {
                        //SU
                        case 1: { nextSelectedSlot = (currentSelectedSlot.navigation.selectOnUp != null) ? nextSelectedSlot.navigation.selectOnUp : null; break; }
                        //GIU
                        case -1: { nextSelectedSlot = (currentSelectedSlot.navigation.selectOnDown != null) ? nextSelectedSlot.navigation.selectOnDown : null; break; }
                    }
                    if (nextSelectedSlot != null)
                    {
                        //DESELEZIONO L'ATTUALE INVENTORY SLOT
                        inventorySlots[CurrentSelectedSlotPosition].Deselect();
                        //AGGIORNO IL CURRENT SELECTED SLOT
                        CurrentSelectedSlotPosition = inventorySlots.IndexOf(nextSelectedSlot.GetComponent<InventoryGUISlot>());

                        //SELEZIONO IL NUOVO ATTUALE INVENTORY SLOT
                        inventorySlots[CurrentSelectedSlotPosition].Select();
                        ShowItemInfo(inventorySlots[CurrentSelectedSlotPosition].PeekItem());
                    }
                }
            }
        }

        protected void ShowItemInfo(Item item)
        {
            if (item == null)
            {
                describablePanel.SetDescription(null);
                describablePanel.gameObject.SetActive(false);
            }
            else
            {
                describablePanel.gameObject.SetActive(true);
                describablePanel.SetDescription(item);
            }
        }
    }
}