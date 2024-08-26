using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(RecipeSlot))]
public class RecipeSlotPlayerInteractions : MonoBehaviour
{
    [SerializeField] PlayerInventory _playerInventory;
    [SerializeField] RecipeSlot _recipeSlot;
    CraftingRecipe.CraftingRecipeShort recipeShort;
    CraftingResourceType[] types;
    int[] num;
    private void Start()
    {
        int numberOfDistinctResources = _recipeSlot.CraftingRecipe.CraftingResources.Distinct().Count();
        types = new CraftingResourceType[numberOfDistinctResources];
         num = new int[numberOfDistinctResources];

        for(int i=0;i< _recipeSlot.CraftingRecipe.CraftingResources.Count();i++)
        {
            if (types.Contains(_recipeSlot.CraftingRecipe.CraftingResources[i])) continue;
            types[i] = _recipeSlot.CraftingRecipe.CraftingResources[i];
        }
        for (int i = 0; i < _recipeSlot.CraftingRecipe.CraftingResources.Count(); i++)
        {
            int index = Array.IndexOf(types, _recipeSlot.CraftingRecipe.CraftingResources[i]);
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
        PickableItem item= Instantiate(_recipeSlot.CraftingRecipe.Result).GetComponent<PickableItem>();
        item.gameObject.SetActive(false);
        item.SetInventory(_playerInventory);
        _playerInventory.PickItemUp(item);
        Debug.Log("Crafted");
    }
}
