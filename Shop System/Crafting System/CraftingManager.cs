using InventorySystem;
using InventorySystem.Items;
using UnityEngine;

namespace ShopSystem.CraftingSystem
{
    [CreateAssetMenu(fileName = "Crafting Manager - ", menuName = "Shop System/Crafting System/Crafting Manager")]
    public class CraftingManager : ScriptableObject, IShop
    {
        [SerializeField]
        private CraftingRecipe[] recipes = null;
        public CraftingRecipe[] Recipes => recipes;

        public int ShopSize => recipes.Length;

        public Item AcquireItem(IInventory inventory, int position)
        {
            //SE SONO IN UNA POSIZIONE OK
            if(position >= 0 && position < recipes.Length)
            {
                //SE POSSO CRAFTARLO, LO CRAFTO, E RITORNO LA DESCRIZIONE DELL'OGGETTO CRAFTATO
                if (Craft(inventory, recipes[position]))
                    return recipes[position].Result;
            }
            //ALTRIMENTI RITORNO NULL
            return null;
        }

        public bool CanCraft(IInventory inventory, CraftingRecipe recipe)
        {
            //RITORNO FALSO SE NON HO ABBASTANZA UNITà DI ALMENO 1 DEI MATERIALI RICHIESTI DALLA RECIPE
            foreach (CraftingItem cItem in recipe.Materials)
                if (inventory.RetrieveItemCount(cItem.Item) < cItem.Amount)
                    return false;
            //SE HO ABBASTANZA MATERIALI PER OGNI MATERIALE RICHIESTO ALLORA RITORNO TRUE
            return true;
        }

        private bool Craft(IInventory inventory, CraftingRecipe recipe)
        {
            if (CanCraft(inventory, recipe))
            {
                //RIMUOVO TUTTI GLI OGGETTI CHE SERVONO X LA RECIPE DALL'INVENTARIO CHE SERVONO
                foreach (CraftingItem cItem in recipe.Materials)
                    for (int cont = 0; cont < cItem.Amount; cont++)
                        Destroy(inventory.RetrieveItem(cItem.Item));
                //INSERISCO NELL'INVENTARIO L'OGGETTO APPENA CRAFTATO (è ISTANZIATO DIRETTAMENTE DALLA CRAFTING RECIPE)
                inventory.InsertItem(recipe.Craft());
                //DICO CHE SON RIUSCITO A CRAFTARE L'OGGETTO
                return true;
            }
            //DICO CHE NON POSSO CRAFTARE L'OGGETTO
            return false;
        }
    }
}