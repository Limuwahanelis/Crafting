using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingResource : MonoBehaviour
{
    public PickableItem AssociatedItem => _associatedItem;
    public CraftingResourceType ResourceType => _resourceType;
    [SerializeField] CraftingResourceType _resourceType;
    [SerializeField] PickableItem _associatedItem;

    private void Start()
    {
        _associatedItem.SetInventorySprite(_resourceType.ResourceSprite);
    }
}
