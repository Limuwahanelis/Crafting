using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
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
    }
    public void ThrowItem(PickableItem item)
    {
        item.transform.position=_throwItemTrans.position;
        item.gameObject.SetActive(true);
    }
}
