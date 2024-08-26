using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Action<PickableItem> OnItemThrown;
    public bool IsTaken => _isTaken;

    [SerializeField] Sprite _emptyIcon;
    [SerializeField] Image _image;
    private PickableItem _item = null;
    private bool _isTaken = false;
    public void SetSlot(PickableItem pickable)
    {
        _isTaken = true;
        _item = pickable;
        _image.sprite = pickable.ItemSprite;
    }
    public void ThrowItemFromSlot()
    {
        OnItemThrown?.Invoke(_item);
        EmptySlot();
    }
    public void EmptySlot()
    {
        _isTaken=false;
        _item =null;
        _image.sprite = _emptyIcon;
    }
}
