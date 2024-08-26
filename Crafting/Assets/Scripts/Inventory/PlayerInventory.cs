using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] Transform _throwItemTrans;
    [SerializeField] List<InventorySlot> _inventorySlots = new List<InventorySlot>();
    private List<PickableItem> _itemsInInventory=new List<PickableItem>();
    private List<CraftingResource> _craftingResources = new List<CraftingResource>();
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
        _craftingResources.Add(resource);
    }
    public void ThrowItem(PickableItem item)
    {
        item.transform.position=_throwItemTrans.position;
        item.gameObject.SetActive(true);
        _itemsInInventory.Remove(item);
        CraftingResource resource = item.GetComponent<CraftingResource>();
        if (resource == null) return;
        _craftingResources.Remove(resource);
    }
    public bool CheckIfInventoryContainsRequiredResources(CraftingRecipe.CraftingRecipeShort recipe)
    {
        for(int i=0;i< recipe.resourceTypes.Count();i++)
        {
            if (_craftingResources.Count((x) => x.ResourceType) != recipe.resourcesNum[i])
            {
                return false;
            }
        }
        return true;
    }
    public void RemoveResourceOfType(CraftingResourceType resourceType)
    {
        CraftingResource resource= _craftingResources.Find((x) => x.ResourceType == resourceType);
        InventorySlot slot= _inventorySlots.Find((x) => x.ItemInSlot==resource.AssociatedItem);
        PickableItem item= slot.ItemInSlot;
        slot.EmptySlot();
        _itemsInInventory.Remove(item);
        _craftingResources.Remove(resource);
        
    }
}
