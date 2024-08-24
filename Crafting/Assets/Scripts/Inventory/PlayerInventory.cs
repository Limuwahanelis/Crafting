using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] List<InventorySlot> _inventorySlots = new List<InventorySlot>();
    public void PickItemUp(PickableItem item)
    {
        InventorySlot slot = _inventorySlots.Find((x) => x.IsTaken == false);
        if (slot!=null)
        {
            slot.SetSlot(item);
        }
    }
}
