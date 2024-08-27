using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftingPopUp : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    public void SetSuccessMessage(PickableItem item)
    {
        _text.text = $"Crafting of {item.Name} was successful";
        gameObject.SetActive(true);
    }
    public void SetFailMessage(CraftingRecipe craftingRecipe)
    {
        _text.text = $"Crafting of {craftingRecipe.name} failed";
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
