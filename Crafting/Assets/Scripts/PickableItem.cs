using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickableItem : MonoBehaviour,IInteractable
{
    public string InteractionDescription { get => _interactionDescription; }

    [SerializeField] string _itemName;
    [SerializeField] string _interactionDescription="Pick up";
    [SerializeField] TMP_Text _text;
    private void Awake()
    {
        _text.text = $"{ InteractionDescription} \n {_itemName}";
    }

    public void Interact()
    {
        gameObject.SetActive(false);
    }

    public void Select()
    {
       _text.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        _text.gameObject.SetActive(false);
    }
}
