using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickableItem : MonoBehaviour,IInteractable,IPickable
{
    public string InteractionDescription { get => _interactionDescription; }
    public string Name { get => _itemName; }
    public string Description { get => _itemDescription; }

    public Sprite ItemSprite => _inInventoryImage;

    [SerializeField] string _itemName;
    [SerializeField] string _itemDescription;
    [SerializeField] string _interactionDescription="Pick up";
    [SerializeField] Sprite _inInventoryImage;
    [SerializeField] TMP_Text _text;
    [SerializeField] PlayerInventory _playerInventory;
    private void Awake()
    {
        _text.text = $"{ InteractionDescription} \n {_itemName}";
    }

    public void Interact()
    {
        PickUp();
    }

    public void Select()
    {
       _text.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        _text.gameObject.SetActive(false);
    }

    public void PickUp()
    {
        gameObject.SetActive(false);
        _playerInventory.PickItemUp(this);
    }
}
