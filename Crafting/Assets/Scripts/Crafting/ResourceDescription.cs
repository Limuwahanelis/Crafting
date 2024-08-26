using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDescription : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] TMP_Text _text;

    public void SetSprite(Sprite sprite)
    {
        _image.sprite = sprite; 
    }
    public void SetDescription(int requiredNumber, int availableNumber, string resourceTypeName)
    {
        if(requiredNumber>availableNumber) _text.color = Color.red;
        else _text.color = Color.green;
        _text.text = $"{availableNumber}/{requiredNumber} x {resourceTypeName}";
    }
}
