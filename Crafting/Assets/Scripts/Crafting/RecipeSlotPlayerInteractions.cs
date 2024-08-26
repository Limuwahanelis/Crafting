using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RecipeSlotPlayerInteractions : MonoBehaviour
{
    [SerializeField] PlayerInventory _playerInventory;
    [SerializeField] CraftingRecipe _craftingRecipe;
    CraftingRecipe.CraftingRecipeShort recipeShort;
    CraftingResourceType[] types;
    int[] num;
    private void Start()
    {
        int numberOfDistinctResources = _craftingRecipe.CraftingResources.Distinct().Count();
        types = new CraftingResourceType[numberOfDistinctResources];
         num = new int[numberOfDistinctResources];

        for(int i=0;i<_craftingRecipe.CraftingResources.Count();i++)
        {
            if (types.Contains(_craftingRecipe.CraftingResources[i])) continue;
            types[i] = _craftingRecipe.CraftingResources[i];
        }
        for (int i = 0; i < _craftingRecipe.CraftingResources.Count(); i++)
        {
            int index = Array.IndexOf(types, _craftingRecipe.CraftingResources[i]);
            num[index]++;
        }
        recipeShort.resourcesNum = num;
        recipeShort.resourceTypes= types;
    }
    public void TryCraft()
    {
        if(!_playerInventory.CheckIfInventoryContainsRequiredResources(recipeShort)) return;
        for(int i=0;i<recipeShort.resourceTypes.Length;i++) 
        {
            for(int j = 0; j < recipeShort.resourcesNum[i];j++)
            {
                _playerInventory.RemoveResourceOfType(recipeShort.resourceTypes[i]);
            }
            
        }
        Debug.Log("Crafted");
    }
}
