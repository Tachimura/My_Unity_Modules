using InventorySystem.GUI;
using InventorySystem.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem
{
    public abstract class AShopGUI: MonoBehaviour
    {
        [SerializeField]
        protected ScrollRect scrollRect = null;
        [SerializeField]
        protected Scrollbar scrollBar = null;
        [SerializeField]
        protected GameObject slotPrefab = null;

        [SerializeField]
        protected TextMeshProUGUI playerMoneyText = null;
        [SerializeField]
        protected TextMeshProUGUI playerOwnedText = null;
        [SerializeField]
        protected TextMeshProUGUI itemCostText = null;

        protected PlayerDataWrapper playerDataWrapper = null;

        public IShop ShopManager { get; protected set; } = null;

        protected IInventoryGUISlot[] shopSlots = null;

        public int InventorySize { get; protected set; } = 0;

        public int CurrentSlotPosition { get; protected set; } = 0;
        public int PreviousSlotPosition { get; protected set; } = 0;

        protected int[] PlayerOwnedCount { get; private set; } = null;

        private void OnDestroy()
        {
            if (playerDataWrapper != null)
            {
                playerDataWrapper.OnPlayerMoneyChanged -= PlayerInventoryMoneyChanged;
                playerDataWrapper.Inventory.OnInventoryItemChanged -= PlayerInventoryItemChanged;
                playerDataWrapper = null;
            }
            ShopManager = null;
            ResetShop();
        }

        public virtual void LoadShop(IShop shop)
        {
            //MI COLLEGO ALL'EVENTO PER MODIFICARE I SOLDI SE CAMBIANO
            if (playerDataWrapper == null)
            {
                playerDataWrapper = PlayerDataManager.Instance.PlayerDataWrapper;
                playerDataWrapper.OnPlayerMoneyChanged += PlayerInventoryMoneyChanged;
                playerDataWrapper.Inventory.OnInventoryItemChanged += PlayerInventoryItemChanged;
            }
            ResetShop();
            //INIT DATI SHOP
            ShopManager = shop;
            PlayerOwnedCount = new int[ShopManager.ShopSize];
            CurrentSlotPosition = 0;
            PreviousSlotPosition = 0;
            //IMPOSTO I SOLDI BASE
            PlayerInventoryMoneyChanged(playerDataWrapper.Money);
        }

        protected abstract void ResetShop();
        public abstract void Move(Vector2 direction);
        public abstract Item AcquireSelectedItem();

         //TODO DA MODIFICARE!
        protected void CenterOnItem(RectTransform item)
        {
            RectTransform selectedRectTransform = item;
            RectTransform scrollRectTransform = scrollRect.viewport.GetComponent<RectTransform>();
            RectTransform contentPanel = scrollRect.content;
            // The position of the selected UI element is the absolute anchor position,
            // ie. the local position within the scroll rect + its height if we're
            // scrolling down. If we're scrolling up it's just the absolute anchor position.
            float selectedPositionY = Mathf.Abs(selectedRectTransform.anchoredPosition.y) + selectedRectTransform.rect.height;
            // The upper bound of the scroll view is the anchor position of the content we're scrolling.
            float scrollViewMinY = contentPanel.anchoredPosition.y;
            // The lower bound is the anchor position + the height of the scroll rect.
            float scrollViewMaxY = contentPanel.anchoredPosition.y + scrollRectTransform.rect.height;

            // If the selected position is below the current lower bound of the scroll view we scroll down.
            if (selectedPositionY > scrollViewMaxY)
            {
                float newY = selectedPositionY - scrollRectTransform.rect.height;
                contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, newY);
            }
            // If the selected position is above the current upper bound of the scroll view we scroll up.
            else if (Mathf.Abs(selectedRectTransform.anchoredPosition.y) < scrollViewMinY)
            {
                contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, Mathf.Abs(selectedRectTransform.anchoredPosition.y));
            }

            scrollBar.size = 0.1f;
            scrollBar.numberOfSteps = InventorySize;
            scrollBar.value = 1 - ((float)CurrentSlotPosition / (float)(InventorySize - 1));
        }

        protected void NormalizeMoveInput(ref Vector2 direction)
        {
            //NORMALIZZO L'INPUT
            //PARTE X, SE SERVE DECOMMENTARE
            /*
            if (direction.x > 0.2)
                direction.x = 1;
            else if (direction.x < -0.2)
                direction.x = -1;
            else
                direction.x = 0;
            */
            //PARTE Y
            if (direction.y > 0.2)
                direction.y = 1;
            else if (direction.y < -0.2)
                direction.y = -1;
            else
                direction.y = 0;
        }

        protected abstract void ShowItemInfo(Item item);

        protected virtual void PlayerInventoryItemChanged(Item item)
        {
            int position = -1;
            bool found = false;
            foreach (IInventoryGUISlot iSlot in shopSlots)
            {
                if (iSlot.PeekItem().EqualsTo(item))
                {
                    position++;
                    found = true;
                    break;
                }
                position++;
            }
            if (position != -1 && found)
                PlayerOwnedCount[position] = playerDataWrapper.Inventory.RetrieveItemCount(item);
        }

        private void PlayerInventoryMoneyChanged(int newAmount)
        {
            //IMPOSTO I SOLDI NEL TESTO, SE è DIVERSO DA NULL
            if (playerMoneyText != null)
                playerMoneyText.text = newAmount.ToString();
        }
    }
}