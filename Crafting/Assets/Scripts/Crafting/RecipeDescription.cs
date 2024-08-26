using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeDescription : MonoBehaviour
{
    [SerializeField] CraftingManager _crafting;
    [SerializeField] List<ResourceDescription> _resourceDescriptions = new List<ResourceDescription>();
    [SerializeField] TMP_Text _successChancetext;
    public void SetDescription(CraftingRecipe.CraftingRecipeShort recipe,int successChance)
    {
        int index = 0;
        CraftingResourceType resourceType;
        for(index = 0; index < recipe.resourceTypes.Length;index++) 
        {
            resourceType= recipe.resourceTypes[index];
            _resourceDescriptions[index].gameObject.SetActive(true);
            _resourceDescriptions[index].SetDescription(recipe.resourcesNum[index], _crafting.GetNumberOfResource(resourceType), resourceType.name);
            _resourceDescriptions[index].SetSprite(resourceType.ResourceSprite);
        }
        for(; index<_resourceDescriptions.Count;index++)
        {
            _resourceDescriptions[index].gameObject.SetActive(false);
        }
        _successChancetext.text = $"Crafting success chance \n {successChance}%";
    }

}
