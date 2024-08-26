using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(RecipeSlot))]
public class RecipeSlotPlayerInteractions : MonoBehaviour
{
    [SerializeField] CraftingManager _crafting;
    [SerializeField] RecipeSlot _recipeSlot;
    [SerializeField] bool _discovered;
    [SerializeField] RecipeDescription _recipeDescription;
    [SerializeField] float _recipeDescriptionDistanceFromMouse = 10f;
    InputActionReference _mousePosAction;
    private bool _isPointerIn = false;
    CraftingRecipe.CraftingRecipeShort recipeShort;
    CraftingResourceType[] types;
    int[] num;
    private Vector3 _recipePos;
    private Vector2 _mousePos;
    private void Start()
    {

        if (!_discovered)
        {
            _recipeSlot.SetSprite(null);
            return;
        }
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
        
    }
    private void Update()
    {
        if(_isPointerIn)
        {
            _recipePos = _mousePos;
            _recipePos.y -= _recipeDescription.GetComponent<RectTransform>().rect.height/2;
            _recipePos.x += _recipeDescription.GetComponent<RectTransform>().rect.width / 2+ _recipeDescriptionDistanceFromMouse;
            _recipeDescription.GetComponent<RectTransform>().position = _recipePos;
        }
    }
    public void SetUp(InputActionReference mousePosAction, CraftingManager crafting,RecipeDescription recipeDescription)
    {
        _crafting = crafting;
        _recipeDescription = recipeDescription;
        _mousePosAction = mousePosAction;
        _mousePosAction.action.performed += UpdateMousePos;
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

    public void DisplayRecipe()
    {
        _isPointerIn = true;
        _recipeDescription.SetDescription(recipeShort);
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
