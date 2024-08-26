using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RecipeSlot))]
public class RecipeSlotPlayerInteractions : MonoBehaviour
{
    //[SerializeField] PlayerInventory _playerInventory;
    [SerializeField] CraftingManager _crafting;
    [SerializeField] RecipeSlot _recipeSlot;
    [SerializeField] bool _discovered;
    [SerializeField] RecipeDescription _recipeDescription;
    [SerializeField] InputActionReference _mousePosAction;
    [SerializeField] float _recipeDescriptionDistanceFromMouse = 10f;
    private bool _isPointerIn = false;
    CraftingRecipe.CraftingRecipeShort recipeShort;
    CraftingResourceType[] types;
    int[] num;
    private Vector3 _recipePos;
    private Vector2 _mousePos;
    private void Start()
    {
        _mousePosAction.action.performed += UpdateMousePos;
        _recipePos = transform.position;
        _recipePos.x += GetComponent<RectTransform>().rect.width / 2 + _recipeDescription.GetComponent<RectTransform>().rect.width / 2;
        int numberOfDistinctResources = _recipeSlot.CraftingRecipe.CraftingResources.Distinct().Count();
        types = new CraftingResourceType[numberOfDistinctResources];
         num = new int[numberOfDistinctResources];
        int j = 0;
        for(int i=0;i< _recipeSlot.CraftingRecipe.CraftingResources.Count();i++)
        {
            if (types.Contains(_recipeSlot.CraftingRecipe.CraftingResources[i])) continue;
            types[j] = _recipeSlot.CraftingRecipe.CraftingResources[i];
            j++;
        }
        for (int i = 0; i < _recipeSlot.CraftingRecipe.CraftingResources.Count(); i++)
        {
            int index = Array.IndexOf(types, _recipeSlot.CraftingRecipe.CraftingResources[i]);
            num[index]++;
        }
        recipeShort.resourcesNum = num;
        recipeShort.resourceTypes= types;
        if (!_discovered) _recipeSlot.SetSprite(null);
    }
    private void Update()
    {
        if(_isPointerIn)
        {
            _recipePos = _mousePos;
            _recipePos.x += _recipeDescription.GetComponent<RectTransform>().rect.width / 2+ _recipeDescriptionDistanceFromMouse;
            _recipeDescription.GetComponent<RectTransform>().position = _recipePos;
        }
    }
    public void TryCraft()
    {
        if (!_discovered) return;
        if(!_crafting.CheckIfContainsRequiredResources(recipeShort)) return;
        for(int i=0;i<recipeShort.resourceTypes.Length;i++) 
        {
            for(int j = 0; j < recipeShort.resourcesNum[i];j++)
            {
                _crafting.RemoveResourceOfType(recipeShort.resourceTypes[i]);
            }
            
        }
        _crafting.CraftItemFromrecipe(_recipeSlot.CraftingRecipe);
        _recipeDescription.SetDescription(recipeShort);
        Debug.Log("Crafted");
    }
    public void SetCraftingManager(CraftingManager crafting)
    {
        _crafting= crafting;
    }
    public void DisplayRecipe()
    {
        _isPointerIn = true;
        _recipeDescription.SetDescription(recipeShort);
        //_recipeDescription.GetComponent<RectTransform>().position = _recipePos;
        _recipeDescription.gameObject.SetActive(true);
    }
    public void HideRecipe()
    {
        _isPointerIn = false;
        _recipeDescription.gameObject.SetActive(false);
    }
    private void UpdateMousePos(InputAction.CallbackContext callback)
    {
        _mousePos = callback.ReadValue<Vector2>();
    }

    private void OnDestroy()
    {
        _mousePosAction.action.performed -= UpdateMousePos;
    }
}
