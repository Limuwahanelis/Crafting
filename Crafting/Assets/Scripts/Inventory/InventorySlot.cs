using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public bool IsTaken => _isTaken;

    [SerializeField] Sprite _emptyIcon;
    [SerializeField] Image _image;
    private bool _isTaken = false;
    private void Start()
    {
        _image.sprite = _emptyIcon;
    }

    public void SetSlot(IPickable pickable)
    {
        _isTaken = true;
        _image.sprite = pickable.ItemSprite;

    }
}
