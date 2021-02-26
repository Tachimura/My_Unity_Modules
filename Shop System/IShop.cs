using InventorySystem;
using InventorySystem.Items;

namespace ShopSystem
{
    public interface IShop
    {
        Item AcquireItem(IInventory inventory, int position);
        int ShopSize { get; }
    }
}