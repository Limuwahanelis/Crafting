using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] CraftingManager _crafting;
    [SerializeField] Transform _throwItemTrans;
    [SerializeField] List<InventorySlot> _inventorySlots = new List<InventorySlot>();
    private List<PickableItem> _itemsInInventory=new List<PickableItem>();
    
    private void Start()
    {
        foreach (InventorySlot slot in _inventorySlots)
        {
            slot.OnItemThrown += ThrowItem;
        }
    }
    public void PickItemUp(PickableItem item)
    {
        InventorySlot slot = _inventorySlots.Find((x) => x.IsTaken == false);
        if (slot!=null)
        {
            slot.SetSlot(item);
            _itemsInInventory.Add(item);
        }
        CraftingResource resource = item.GetComponent<CraftingResource>();
        if (resource == null) return;
        _crafting.AddResource(resource);
    }
    public void ThrowItem(PickableItem item)
    {
        item.transform.position=_throwItemTrans.position;
        item.ResetModelPos();
        item.gameObject.SetActive(true);
        _itemsInInventory.Remove(item);
        CraftingResource resource = item.GetComponent<CraftingResource>();
        if (resource == null) return;
        _crafting.RemoveResource(resource);
    }
    public void RemoveResource(CraftingResource resource)
    {
        InventorySlot slot = _inventorySlots.Find((x) => x.ItemInSlot == resource.AssociatedItem);
        PickableItem item = slot.ItemInSlot;
        slot.EmptySlot();
        _itemsInInventory.Remove(item);
    }
    public void OnItemCrafted(PickableItem item)
    {
        item.SetInventory(this);
        PickItemUp(item);
    }
}
