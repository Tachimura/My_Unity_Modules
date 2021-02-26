using InventorySystem.Items;
using System;
using UnityEngine;

namespace ShopSystem.CraftingSystem
{
    [CreateAssetMenu(fileName = "Crafting Recipe - ", menuName = "Shop System/Crafting System/Crafting Recipe")]
    public class CraftingRecipe : ScriptableObject
    {
        private static readonly int CraftingRecipeMaxSize = 6;
        [Header("Recipe Input")]
        [SerializeField]
        private CraftingItem[] materials = null;
        public CraftingItem[] Materials => materials;

        [Header("Recipe Output")]
        [SerializeField]
        private Item result = null;
        public Item Result => result;

        //CREO UNA ISTANZA DELL'OGGETTO RISULTANTE
        public Item Craft() => Instantiate(result);

        private void OnValidate()
        {
            //SE IL NUMERO DI MATERIALI O RISULTATI è MAGGIORE DI 6, LO FORZO A 6
            if (materials != null && materials.Length > CraftingRecipeMaxSize)
                Array.Resize(ref materials, CraftingRecipeMaxSize);
        }
    }
}