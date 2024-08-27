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
    [SerializeField] GameObject _model;
    private void Awake()
    {
        _text.text = $"{ InteractionDescription} \n {_itemName}";
    }
    public void ResetModelPos()
    {
        _model.transform.localPosition = Vector3.zero;
    }
    public void Interact()
    {
        PickUp();
    }

    public void Select()
    {
        // for some reason disabling and enabling gameobject from script disables update function in script so we disabe text component instead.
        _text.enabled = true;
    }

    public void Deselect()
    {
        _text.enabled=false;
    }
    public void SetDescriptionParent(Transform parent)
    {
        _text.transform.SetParent(parent);
    }
    public void SetInventory(PlayerInventory inventory)
    {
        _playerInventory = inventory;
    }
    public void SetInventorySprite(Sprite sprite)
    {
        _inInventoryImage = sprite;
    }
    public void PickUp()
    {
        if(_playerInventory.PickItemUp(this))
        {
            gameObject.SetActive(false);
        }
        
    }
}
