using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{
    [SerializeField] CraftingRecipe _craftingRecipe;
    [SerializeField] Image _image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnValidate()
    {
        _image.sprite = _craftingRecipe.ResultSprite;
    }
}
