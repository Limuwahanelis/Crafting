using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{

    public CraftingRecipe CraftingRecipe => _craftingRecipe;
    [SerializeField] CraftingRecipe _craftingRecipe;
    [SerializeField] Image _image;
    

    public void SetSprite(Sprite sprite)
    {
        _image.sprite = sprite;
    }
    private void OnValidate()
    {
        if(_image != null && _craftingRecipe!=null) _image.sprite = _craftingRecipe.ResultSprite;
    }
}
