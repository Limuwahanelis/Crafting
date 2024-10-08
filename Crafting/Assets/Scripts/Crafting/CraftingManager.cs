using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] InputActionReference _mousePosAction;
    [SerializeField] RecipeDescription _recipeDescription;
    [SerializeField] Transform _runtimeDescriptionHolder;
    [SerializeField] List<CraftingResource> _craftingResources= new List<CraftingResource>();
    [SerializeField] List<GameObject> _recipeGrids=new List<GameObject>();
    [SerializeField] List<RecipeSlot> _recipeSlots=new List<RecipeSlot>();
    public UnityEvent<CraftingResource> OnCraftingResourceRemoved;
    public UnityEvent<PickableItem> OnItemCrafted;
    public UnityEvent<CraftingRecipe> OnItemCraftingFailed;
    
    private void Start()
    {
        _recipeSlots.Clear();
        for (int i = 0; i < _recipeGrids.Count; i++)
        {
            _recipeSlots.AddRange(_recipeGrids[i].GetComponentsInChildren<RecipeSlot>());
        }
        for (int i = 0; i < _recipeSlots.Count; i++)
        {
            _recipeSlots[i].GetComponent<RecipeSlotPlayerInteractions>().SetUp(_mousePosAction,this,_recipeDescription);
        }
    }
    public void AddResource(CraftingResource resource)
    {
        _craftingResources.Add(resource);
    }
    public void RemoveResource(CraftingResource resource)
    {
        _craftingResources.Remove(resource);
    }
    public int GetNumberOfResource(CraftingResourceType resourceType)
    {
        return _craftingResources.Count(x => x.ResourceType == resourceType);
    }
    public bool CheckIfContainsRequiredResources(CraftingRecipe.CraftingRecipeShort recipe)
    {
        for (int i = 0; i < recipe.resourceTypes.Count(); i++)
        {
            if (_craftingResources.Count(x => x.ResourceType == recipe.resourceTypes[i]) < recipe.resourcesNum[i])
            {
                return false;
            }
        }
        return true;
    }
    public void RemoveResourceOfType(CraftingResourceType resourceType)
    {
        CraftingResource resource = _craftingResources.Find((x) => x.ResourceType == resourceType);
        _craftingResources.Remove(resource);
        OnCraftingResourceRemoved?.Invoke(resource);
    }
    public void CraftItemFromRecipe(CraftingRecipe recipe)
    {
        float number= UnityEngine.Random.Range(0, 100);
        if (number < recipe.SuccessChance)
        {
            PickableItem item = Instantiate(recipe.Result).GetComponent<PickableItem>();
            item.gameObject.SetActive(false);
            item.SetDescriptionParent(_runtimeDescriptionHolder);
            OnItemCrafted?.Invoke(item);
        }
        else OnItemCraftingFailed?.Invoke(recipe);

    }
    private void OnDisable()
    {
        _recipeDescription.gameObject.SetActive(false );
    }
}
